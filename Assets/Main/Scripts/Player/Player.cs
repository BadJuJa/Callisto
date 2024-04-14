using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using System.Collections;
using UnityEngine;

namespace BadJuja.Player {
    public class Player : CharacterCore, IDamagable {

        public float PlayerStatCheckDelay = 1f;

        private float _currentExperience = 0;
        private float _lastMaxHealth;

        private int _level = 1;
        private float baseXpForLevel = 100f;
        private float xpForLevelMult = 1.25f;
        private float _xpToNextLevel {
            get {
                return _level * baseXpForLevel * xpForLevelMult;
            }
        }

        public float GetDamage => Damage.Value;

        private void OnDisable()
        {
            GlobalEvents.OnEnemyDied -= AddExperience;
        }

        private void OnEnable()
        {
            StartCoroutine(PlayerStatsCheck());
            GlobalEvents.OnEnemyDied += AddExperience;
        }

        protected override void Awake()
        {
            base.Awake();

            _lastMaxHealth = Health.Value;

            GlobalEvents.Send_OnPlayerHealthChanged(_currentHealth, Health.Value);
        }

        public override void TakeDamage(float value)
        {
            value *= 1 - DamageReduction.Value / 100;
            if (value < Health.Value)
            {
                _currentHealth -= value;
            }
            else
            {
                _currentHealth = 0;
            }

            if (_currentHealth <= 0)
            {
                Die();
            }

            GlobalEvents.Send_OnPlayerHealthChanged(_currentHealth, Health.Value);
        }

        protected override void Die()
        {
            base.Die();
        }

        public void ApplyStatMod(AllCharacterStats stat, StatModType type, float strenght, object source = null)
        {
            Stats[stat].AddModifier(new StatModifier(strenght, type, source ?? this));
            _ = Stats[stat].Value;
        }

        public void RemoveStatMod(AllCharacterStats stat, object source)
        {
            Stats[stat].RemoveAllModifiersFromSource(source);
        }

        private void AddExperience(float xpValue)
        {
            _currentExperience += xpValue;
            if (_currentExperience >= _xpToNextLevel)
            {
                _currentExperience -= _xpToNextLevel;
                _level++;
                GlobalEvents.Send_OnPlayerLevelIncreased();
            }
            GlobalEvents.Send_OnPlayerExperienceChanged(_currentExperience, _xpToNextLevel);
        }

        private IEnumerator PlayerStatsCheck()
        {
            while (true)
            {
                yield return new WaitForSeconds(PlayerStatCheckDelay);

                if (Health.Value != _lastMaxHealth)
                {
                    float difference = Health.Value - _lastMaxHealth;
                    _lastMaxHealth += difference;
                    if (_currentHealth + difference <= 0)
                    {
                        _currentHealth = 1f;
                    }
                    else
                    {
                        _currentHealth += difference;
                    }
                    GlobalEvents.Send_OnPlayerHealthChanged(_currentHealth, Health.Value);
                }
            }
        }
    }
}