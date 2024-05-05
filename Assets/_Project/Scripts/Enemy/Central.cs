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

        private float _currentHealth;
        
        public float HealthPercentage => _currentHealth / _stats.GetStatValue(AllCharacterStats.Health);
        public bool PlayerInReachDistance => _enemyFieldOfView.PlayerInReachDistance;
        public EnemyData EnemyData { get => _data; private set => _data = value; }
        public Transform PlayerTransform { get; private set; }

        public void Initialize(EnemyData data)
        {
            _data = data;
            object a = new();
            
            _enemyMovement = GetComponent<Movement>();

            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            _stats.Init();

            if (_data.BaseStatsOverride != null)
            {
                var modList = _data.BaseStatsOverride.CharacterModifiers;
                foreach (var mod in modList)
                {
                    StatModifier newStatMod = new(mod.ModType == StatModType.PercentAdd ? mod.Strenght / 100 : mod.Strenght, mod.ModType, this);
                    _stats.ApplyMod(mod.Stat, newStatMod);
                }
            }

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

        public void TakeDamage(float value)
        {
            value = Mathf.Clamp(value - _stats.GetStatValue(AllCharacterStats.Armor), 1, value);

            if (value < _currentHealth)
                _currentHealth -= value;
            else
                Die();

            healthBar.UpdateHealth(HealthPercentage);
        }

        private void Die()
        {
            EnemyRelatedEvents.Send_OnEnemyDied(EnemyData.ExperienceReward);
            Destroy(gameObject);
        }
    }
}