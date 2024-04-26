using BadJuja.Core.CharacterStats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data
{
    [Serializable]
    public struct CharacterBonusModifier
    {
        public AllCharacterStats Stat;
        public StatModType ModType;
        public float Strenght;
    }

    [CreateAssetMenu(fileName = "Player Current Equipment", menuName = "Data/Player Current Equipment")]
    public class PlayerCurrentEquipment : ScriptableObject
    {
        [Header("Light Weapon")]
        public RangedWeaponData LightRangedWeaponData;
        public WeaponElements LightRangedWeaponElement;

        [Header("Heavy Weapon")]
        public RangedWeaponData HeavyRangedWeaponData;
        public WeaponElements HeavyRangedWeaponElement;

        [Header("Melee Weapon")]
        public MeleeWeaponData MeleeWeaponData;
        public WeaponElements MeleeWeaponElement;

        [Header("Character")]
        public List<CharacterBonusModifier> CharacterBonusModifiers = new();
    }
}
