using BadJuja.Core;
using BadJuja.Core.Events;
using BadJuja.Core.CharacterStats;
using UnityEngine;
using BadJuja.Core.Data;

namespace BadJuja.Player {
    public class Player : MonoBehaviour, IDamagable, IStats {

        [Header("General")]
        public float StatCheckDelay = 1f;

        [Header("Stats")]
        [SerializeField] private CharacterStatStructure _stats = new();

        float IStats.GetStatValue(AllCharacterStats stat) => _stats.GetStatValue(stat);
        float IStats.GetElementalResistance(WeaponElements damageElement) => _stats.GetElementalResistance(damageElement);
        float IStats.GetElementalBonus(WeaponElements damageElement) => _stats.GetElementalBonus(damageElement);

        private float _lastMaxHealth;
        private float _currentHealth = 0;

        #region Experience

        private int _level = 1;
        private int _currentExperience = 0;
        private int _baseXpForLevel = 100;
        private float _xpForLevelMult = 1.25f;

        private int XpToNextLevel {
            get {
                return (int)(_level * _baseXpForLevel * _xpForLevelMult);
            }
        }
        private void AddExperience(int xpValue)
        {
            _currentExperience += xpValue;
            if (_currentExperience >= XpToNextLevel)
            {
                _currentExperience -= XpToNextLevel;
                _level++;
                PlayerRelatedEvents.Send_OnLevelIncreased();
            }
            
            PlayerRelatedEvents.Send_OnExperienceChanged(_currentExperience, XpToNextLevel);
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
                ApplyStatMod(mod.Stat, new(mod.Strenght, mod.ModType, this));
            }

            SubscribeToEvents();
        }

        private void Start()
        {
            PlayerRelatedEvents.Send_OnHealthChanged(_currentHealth, _lastMaxHealth);
        }

        public Visuals.Model InstantiateModel(GameObject model)
        {
            var go = Instantiate(model, transform, false);
            return go.GetComponent<Visuals.Model>();
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

        public void TakeDamage(float value, WeaponElements damageElement)
        {
            value = ApplyResistance(value, damageElement);

            if (value < _currentHealth)
                _currentHealth -= value;
            else
                Die();

            PlayerRelatedEvents.Send_OnHealthChanged(_currentHealth, _stats.GetStatValue(AllCharacterStats.Health));
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
            PlayerRelatedEvents.Send_OnDeath();
        }

        private void ApplyStatMod(AllCharacterStats stat, StatModifier mod, object source = null)
        {
            if (mod.Source == null)
                mod = new(mod.Value, mod.Type, source ?? this);

            if (stat == AllCharacterStats.Damage)
            {
                if (mod.Source.GetType() == typeof(Combat))
                    mod = new(mod.Value, mod.Type, mod.Source);
                else
                    mod = new(mod.Value / 100, StatModType.PercentAdd, mod.Source);
            }

            _stats.ApplyMod(stat, mod);

            if (stat == AllCharacterStats.Health)
                PlayerStatsCheck();
        }

        private void RemoveStatMod(AllCharacterStats stat, object source)
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
                PlayerRelatedEvents.Send_OnHealthChanged(_currentHealth, _stats.GetStatValue(AllCharacterStats.Health));
            }
        }
    }
}