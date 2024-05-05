using BadJuja.Core;
using BadJuja.Core.Events;
using BadJuja.Core.CharacterStats;
using UnityEngine;
using BadJuja.Core.Data;

namespace BadJuja.Player {
    public class Player : MonoBehaviour, IDamagable, IStats {

        [Header("General")]
        public float PlayerStatCheckDelay = 1f;

        [Header("Stats")]
        [SerializeField] private CharacterStatStructure _stats = new();
        public float GetStatValue(AllCharacterStats stat)
        {
            return _stats.GetStatValue(stat);
        }
        private float _lastMaxHealth;
        private float _currentHealth = 0;

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
        

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void Initialize(PlayerCurrentEquipment playerCurrentEquipment)
        {
            _stats.Init();

            _currentHealth = _stats.GetStatValue(AllCharacterStats.Health);
            _lastMaxHealth = _currentHealth;

            var modList = playerCurrentEquipment.CharacterBonusModifiers.CharacterModifiers;
            foreach (var mod in modList)
            {
                StatModifier newStatMod = new(mod.Strenght, mod.ModType, this);
                ApplyStatMod(mod.Stat, newStatMod);
            }

            SubscribeToEvents();
        }

        private void Start()
        {
            PlayerRelatedEvents.Send_OnPlayerHealthChanged(_currentHealth, _lastMaxHealth);
        }

        public PlayerModel InstantiateModel(GameObject model)
        {
            var go = Instantiate(model, transform, false);
            return go.GetComponent<PlayerModel>();
        }

        private void SubscribeToEvents()
        {
            EnemyRelatedEvents.OnEnemyDied += AddExperience;
            ModifiersRelaterEvents.ApplyModifierToPlayer += ApplyStatMod;
            ModifiersRelaterEvents.RemovePlayerModifier += RemoveStatMod;
        }

        private void UnsubscribeFromEvents()
        {
            EnemyRelatedEvents.OnEnemyDied -= AddExperience;
            ModifiersRelaterEvents.ApplyModifierToPlayer -= ApplyStatMod;
            ModifiersRelaterEvents.RemovePlayerModifier -= RemoveStatMod;
        }

        public void TakeDamage(float value)
        {
            value = Mathf.Clamp(value - _stats.GetStatValue(AllCharacterStats.Armor), 1, value);

            if (value < _currentHealth)
                _currentHealth -= value;
            else
                Die();

            PlayerRelatedEvents.Send_OnPlayerHealthChanged(_currentHealth, _stats.GetStatValue(AllCharacterStats.Health));
        }

        private void Die()
        {

        }

        public void ApplyStatMod(AllCharacterStats stat, StatModifier mod, object source = null)
        {
            if (mod.Source == null)
                mod = new(mod.Value, mod.Type, this);
            
            _stats.ApplyMod(stat, mod);

            if (stat == AllCharacterStats.Health)
                PlayerStatsCheck();
        }

        public void RemoveStatMod(AllCharacterStats stat, object source)
        {
            _stats.RemoveMod(stat, source);

            if (stat == AllCharacterStats.Health)
                PlayerStatsCheck();
        }

        private void PlayerStatsCheck()
        {
            if (_stats.GetStatValue(AllCharacterStats.Health) != _lastMaxHealth)
            {
                float difference = _stats.GetStatValue(AllCharacterStats.Health) - _lastMaxHealth;
                _lastMaxHealth += difference;
                if (_currentHealth + difference <= 0)
                {
                    _currentHealth = 1f;
                }
                else
                {
                    _currentHealth += difference;
                }
                PlayerRelatedEvents.Send_OnPlayerHealthChanged(_currentHealth, _stats.GetStatValue(AllCharacterStats.Health));
            }
        }

        
    }
}