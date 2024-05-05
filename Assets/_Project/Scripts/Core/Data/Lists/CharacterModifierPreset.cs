using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data.Lists
{
    [Serializable]
    public struct CharacterBonusModifier {
        public AllCharacterStats Stat;
        public CharacterStats.StatModType ModType;
        public float Strenght;
    }

    [CreateAssetMenu(fileName = "New Modifiers Preser", menuName = "Data/Stats/Modifiers Preset")]
    public class CharacterModifierPreset : ScriptableObject
    {
        public List<CharacterBonusModifier> CharacterModifiers = new();
    }
}
