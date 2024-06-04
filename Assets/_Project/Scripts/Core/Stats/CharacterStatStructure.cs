using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.CharacterStats
{
    [Serializable]
    public class CharacterStatStructure
    {
        private bool isInitialized = false;

        [Header("Stats")]
        [SerializeField] private CharacterStat Health = new(100);
        [SerializeField] private CharacterStat Armor = new(0);
        [SerializeField] private CharacterStat MovementSpeed = new(5);
        [SerializeField] private CharacterStat Damage = new(0);
        [SerializeField] private CharacterStat CritChance = new(5);
        [SerializeField] private CharacterStat CritDamage = new(135);
        [SerializeField] private CharacterStat FireDamage = new(0);
        [SerializeField] private CharacterStat FrostDamage = new(0);
        [SerializeField] private CharacterStat ShockDamage = new(0);
        [SerializeField] private CharacterStat FireResistance = new(0, 80);
        [SerializeField] private CharacterStat FrostResistance = new(0, 80);
        [SerializeField] private CharacterStat ShockResistance = new(0, 80);

        protected Dictionary<AllCharacterStats, CharacterStat> StatsDict;
        protected Dictionary<WeaponElements, AllCharacterStats> ElementalResistanceDict;
        protected Dictionary<WeaponElements, AllCharacterStats> ElementalBonusDict;

        public float GetStatValue(AllCharacterStats characterStat)
        {
            if (!isInitialized)
                return 0;
            return StatsDict[characterStat].Value; ;
        }
        public float GetElementalResistance(WeaponElements damageElement)
        {
            if (ElementalResistanceDict.TryGetValue(damageElement, out AllCharacterStats resistanceStat)) 
                return GetStatValue(resistanceStat);
            else 
                return 0;
        }
        public float GetElementalBonus(WeaponElements damageElement)
        {
            if (ElementalBonusDict.TryGetValue(damageElement, out AllCharacterStats bonusStat))
                return GetStatValue(bonusStat);
            else
                return 0;
        }
        public void ApplyMod(AllCharacterStats stat, StatModifier mod)
        {
            StatsDict[stat].AddModifier(mod);
            GetStatValue(stat);
        }
        public bool RemoveMod(AllCharacterStats stat, object souce)
        {
            bool returnValue = StatsDict[stat].RemoveAllModifiersFromSource(souce);
            GetStatValue(stat);
            return returnValue;
        }
        public void Initialize()
        {
            FillDictionaries();

            isInitialized = true;
        }
        private void FillDictionaries()
        {
            StatsDict = new()
            {
                { AllCharacterStats.Health, Health },
                { AllCharacterStats.Armor, Armor },
                { AllCharacterStats.MovementSpeed, MovementSpeed },
                { AllCharacterStats.Damage, Damage },
                { AllCharacterStats.CritChance, CritChance },
                { AllCharacterStats.CritDamage, CritDamage },
                { AllCharacterStats.FireDamage, FireDamage },
                { AllCharacterStats.FrostDamage, FrostDamage },
                { AllCharacterStats.ShockDamage, ShockDamage },
                { AllCharacterStats.FireResistance, FireResistance },
                { AllCharacterStats.FrostResistance, FrostResistance },
                { AllCharacterStats.ShockResistance, ShockResistance },
            };

            ElementalResistanceDict = new()
            {
                { WeaponElements.Fire, AllCharacterStats.FireResistance },
                { WeaponElements.Frost, AllCharacterStats.FrostResistance },
                { WeaponElements.Shock, AllCharacterStats.ShockResistance },
            };

            ElementalBonusDict = new()
            {
                { WeaponElements.Fire, AllCharacterStats.FireDamage },
                { WeaponElements.Frost, AllCharacterStats.FrostDamage },
                { WeaponElements.Shock, AllCharacterStats.ShockDamage },
            };
        }
    }
}
