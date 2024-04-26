using BadJuja.Core;
using BadJuja.Core.Events;
using BadJuja.Core.CharacterStats;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player {
    public class Player : MonoBehaviour, IDamagable {
        [Header("General")]
        public float PlayerStatCheckDelay = 1f;

        #region Stats
        [Header("Stats")]
        [SerializeField] private CharacterStat Health = new();
        [SerializeField] private CharacterStat Armor = new();
        [SerializeField] private CharacterStat MovementSpeed = new();

        [SerializeField] private CharacterStat Damage = new();

        [SerializeField] private CharacterStat CritChance = new();
        [SerializeField] private CharacterStat CritDamage = new();

        [SerializeField] private CharacterStat FireDamage = new();
        [SerializeField] private CharacterStat FrostDamage = new();
        [SerializeField] private CharacterStat ShockDamage = new();

        [SerializeField] private CharacterStat FireResistance = new();
        [SerializeField] private CharacterStat FrostResistance = new();
        [SerializeField] private CharacterStat ShockResistance = new();


        protected Dictionary<AllCharacterStats, CharacterStat> Stats = new();
        private void FillStatsDictionary()
        {
            Stats.Add(AllCharacterStats.Health, Health);
            Stats.Add(AllCharacterStats.Armor, Armor);
            Stats.Add(AllCharacterStats.MovementSpeed, MovementSpeed);

            Stats.Add(AllCharacterStats.Damage, Damage);

            Stats.Add(AllCharacterStats.CritChance, CritChance);
            Stats.Add(AllCharacterStats.CritDamage, CritDamage);

            Stats.Add(AllCharacterStats.FireDamage, FireDamage);
            Stats.Add(AllCharacterStats.FrostDamage, FrostDamage);
            Stats.Add(AllCharacterStats.ShockDamage, ShockDamage);
        }
        #endregion

        #region Experience

        private int _level = 1;
        private float _currentExperience = 0;
        private float baseXpForLevel = 100f;
        private float xpForLevelMult = 1.25f;
        private float _xpToNextLevel {
            get {
                return _level * baseXpForLevel * xpForLevelMult;
            }
        }

        private void AddExperience(float xpValue)
        {
            _currentExperience += xpValue;
            if (_currentExperience >= _xpToNextLevel)
            {
                _currentExperience -= _xpToNextLevel;
                _level++;
                PlayerRelatedEvents.Send_OnPlayerLevelIncreased();
            }
            PlayerRelatedEvents.Send_OnPlayerExperienceChanged(_currentExperience, _xpToNextLevel);
        }

        #endregion

        private float _lastMaxHealth;
        private float _currentHealth = 0;

        public float GetDamage => Damage.Value;
        public float GetFireResistance => FireResistance.Value;
        public float GetFrostResistance => FrostResistance.Value;
        public float GetShockResistance => ShockResistance.Value;

        private void OnDisable()
        {
            EnemyRelatedEvents.OnEnemyDied -= AddExperience;
        }

        private void OnEnable()
        {
            EnemyRelatedEvents.OnEnemyDied += AddExperience;
        }

        private void Awake()
        {
            FillStatsDictionary();

            _currentHealth = Health.Value;
            _lastMaxHealth = Health.Value;

            PlayerRelatedEvents.Send_OnPlayerHealthChanged(_currentHealth, Health.Value);

            PlayerRelatedEvents.Send_PlayerInitiated();
        }

        public void TakeDamage(float value)
        {
            value = Mathf.Clamp(value - Armor.Value, 1, value);

            if (value < _currentHealth)
                _currentHealth -= value;
            else
                Die();

            PlayerRelatedEvents.Send_OnPlayerHealthChanged(_currentHealth, Health.Value);
        }

        private void Die()
        {
        }

        public void ApplyStatMod(AllCharacterStats stat, StatModType type, float strenght, object source = null)
        {
            Stats[stat].AddModifier(new StatModifier(strenght, type, source ?? this));
            _ = Stats[stat].Value;

            if (stat == AllCharacterStats.Health)
                PlayerStatsCheck();
        }

        public void RemoveStatMod(AllCharacterStats stat, object source)
        {
            Stats[stat].RemoveAllModifiersFromSource(source);

            if (stat == AllCharacterStats.Health)
                PlayerStatsCheck();
        }

        private void PlayerStatsCheck()
        {
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
                PlayerRelatedEvents.Send_OnPlayerHealthChanged(_currentHealth, Health.Value);
            }
        }
    }
}