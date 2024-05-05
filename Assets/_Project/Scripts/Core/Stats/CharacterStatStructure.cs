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

        protected Dictionary<AllCharacterStats, CharacterStat> Stats = new();

        public float GetStatValue(AllCharacterStats characterStat)
        {
            if (!isInitialized)
                return 0;
            var value = Stats[characterStat].Value;
            return value;
        }

        public void Init()
        {
            FillStatsDictionary();
            
            for (int i = 0; i < Enum.GetValues(typeof(AllCharacterStats)).Length; i++)
            {
                _ = Stats[(AllCharacterStats)i].Value;
            }

            isInitialized = true;
        }
        
        public void ApplyMod(AllCharacterStats stat, StatModifier mod)
        {
            Stats[stat].AddModifier(mod);
            GetStatValue(stat);
        }

        public bool RemoveMod(AllCharacterStats stat, object souce)
        {
            bool returnValue = Stats[stat].RemoveAllModifiersFromSource(souce);
            GetStatValue(stat);
            return returnValue;
        }

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

            Stats.Add(AllCharacterStats.FireResistance, FireResistance);
            Stats.Add(AllCharacterStats.FrostResistance, FrostResistance);
            Stats.Add(AllCharacterStats.ShockResistance, ShockResistance);
        }
    }
}
