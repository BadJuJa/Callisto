using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Enemy {
    public class Central : MonoBehaviour, IEnemyCentral, IEnemyInit, IDamagable, IStats {

        [Header("References")]
        public HealthBar healthBar;
        public Transform AttackPoint;

        private EnemyData _data;
        private FieldOfView _enemyFieldOfView = new();
        private Movement _enemyMovement;
        private AttackBase _enemyAttack;
        
        [Header("Stats")]
        private CharacterStatStructure _stats = new();

        public float GetStatValue(AllCharacterStats stat) => _stats.GetStatValue(stat);
        public float GetElementalResistance(WeaponElements damageElement) => _stats.GetElementalResistance(damageElement);
        public float GetElementalBonus(WeaponElements damageElement) => _stats.GetElementalBonus(damageElement);

        private float _currentHealth;
        
        public float HealthPercentage => _currentHealth / _stats.GetStatValue(AllCharacterStats.Health);
        public bool PlayerInReachDistance => _enemyFieldOfView.PlayerInReachDistance;
        public EnemyData EnemyData { get => _data; private set => _data = value; }
        public Transform PlayerTransform { get; private set; }

        public void Initialize(EnemyData data)
        {
            _data = data;
            
            _enemyMovement = GetComponent<Movement>();

            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            _stats.Init();

            if (_data.StatsOverride != null)
            {
                var modList = _data.StatsOverride.Overrides;
                foreach (var mod in modList)
                {
                    if (mod.Stat == AllCharacterStats.Damage)
                        _stats.ApplyMod(mod.Stat, new(mod.Strenght / 100, StatModType.PercentAdd, this));
                    else
                        _stats.ApplyMod(mod.Stat, new(mod.Strenght, mod.ModType, this));
                }
            }
            _stats.ApplyMod(AllCharacterStats.Damage, new(_data.Damage, StatModType.Flat, this));

            _currentHealth = _stats.GetStatValue(AllCharacterStats.Health);

            _enemyFieldOfView.Initiate(transform, _data.AttackRange, context: this);

            _enemyMovement.SetSpeed(_stats.GetStatValue(AllCharacterStats.MovementSpeed));

            GameObject model = Instantiate(original: _data.Prefab, parent: gameObject.transform, rotation: Quaternion.identity, position: transform.position);
            AnimatorControllerScript animatorController = model.GetComponent<AnimatorControllerScript>();
            
            if (_data.EnemyType == EnemyTypes.Melee)
                _enemyAttack = new AttackMelee(this, this);
            else
                _enemyAttack = new AttackRanged(transform, this, this);

            animatorController.Initialize(_enemyAttack);

            _enemyAttack.Initialize(_data, this, AttackPoint, animatorController);
        }

        public void TakeDamage(float value, WeaponElements damageElement)
        {
            value = ApplyResistance(value, damageElement);

            if (value < _currentHealth)
                _currentHealth -= value;
            else
                Die();

            healthBar.UpdateHealth(HealthPercentage);
        }

        private float ApplyResistance(float value = 0, WeaponElements damageElement = WeaponElements.Physical)
        {
            switch (damageElement)
            {
                case WeaponElements.Fire:
                    value *= 1 - _stats.GetStatValue(AllCharacterStats.FireResistance) / 100;
                    break;
                case WeaponElements.Frost:
                    value *= 1 - _stats.GetStatValue(AllCharacterStats.FrostResistance) / 100;
                    break;
                case WeaponElements.Shock:
                    value *= 1 - _stats.GetStatValue(AllCharacterStats.ShockResistance) / 100;
                    break;
                case WeaponElements.Physical:
                    value = Mathf.Clamp(value - _stats.GetStatValue(AllCharacterStats.Armor), 1, value);
                    break;
            }
            return value;
        }

        private void Die()
        {
            EnemyRelatedEvents.Send_OnEnemyDied((int)EnemyData.ExperienceReward);
            Destroy(gameObject);
        }
    }
}