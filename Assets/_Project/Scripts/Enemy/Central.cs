using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Enemy {
    public class Central : MonoBehaviour, IEnemyCentral, IEnemyInitialize, IDamagable, IStats {

        [Header("References")]
        public HealthBar healthBar;
        public Transform AttackPoint;

        private EnemyData _data;
        private FieldOfView _fieldOfView = new();
        private CombatBase _combatComponent;
        private float _currentHealth;

        [Header("Stats")]
        private CharacterStatStructure _stats = new();
        public float GetStatValue(AllCharacterStats stat) => _stats.GetStatValue(stat);
        public float GetElementalResistance(WeaponElements damageElement) => _stats.GetElementalResistance(damageElement);
        public float GetElementalBonus(WeaponElements damageElement) => _stats.GetElementalBonus(damageElement);
        
        public float HealthPercentage => _currentHealth / _stats.GetStatValue(AllCharacterStats.Health);
        public bool PlayerInReachDistance => _fieldOfView.PlayerInReachDistance;
        public Transform PlayerTransform { get; private set; }

        public void Initialize(EnemyData data)
        {
            _data = data;
            
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            _stats.Initialize();

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

            GetComponent<Movement>().SetSpeed(_stats.GetStatValue(AllCharacterStats.MovementSpeed));
            
            _fieldOfView.Initiate(transform, _data.AttackRange, context: this);

            GameObject model = Instantiate(original: _data.Prefab, parent: gameObject.transform, rotation: Quaternion.identity, position: transform.position);
            AnimatorControllerScript animatorController = model.GetComponent<AnimatorControllerScript>();
            
            if (_data.EnemyType == EnemyTypes.Melee)
                _combatComponent = new CombatMelee(this, this);
            else
                _combatComponent = new CombatRanged(transform, this, this);

            animatorController.Initialize(_combatComponent);

            _combatComponent.Initialize(_data, this, AttackPoint, animatorController);
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
            if (damageElement == WeaponElements.Physical)
                value = Mathf.Clamp(value - _stats.GetStatValue(AllCharacterStats.Armor), 1, value);
            else
                value *= 1 - _stats.GetElementalResistance(damageElement) / 100;

            return value;
        }

        private void Die()
        {
            EnemyRelatedEvents.Send_OnEnemyDied((int)_data.ExperienceReward);
            Destroy(gameObject);
        }
    }
}