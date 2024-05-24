using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data.Lists {

    [CreateAssetMenu(fileName = "New Stat Override", menuName = "Data/Enemy/Stats Override")]
    public class EnemyStatsOverridePreset : ScriptableObject
    {
        [Serializable]
        public class EnemyStat {
            public AllCharacterStats Stat;
            public float Strenght;
            public CharacterStats.StatModType ModType => CharacterStats.StatModType.Flat;
        }

        public List<EnemyStat> Overrides = new();
    }
}