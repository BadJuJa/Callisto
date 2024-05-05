using UnityEngine;

namespace BadJuja.Core.CharacterStats
{
    [CreateAssetMenu(fileName = "New Stat Upgrade Rarity", menuName = "Data/Stats/Stat Upgrade Rarity")]
    public class StatUpgradeRarity : ScriptableObject
    {
        public string Name;

        [Tooltip("Float between 0 and 1")]
        public float Chance;
    }
}
