using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.CharacterStats
{

    [Serializable]
    public struct UpgradeRarity
    {
        public StatUpgradeRarity Rarity;
        public float Strength;
    }

    [CreateAssetMenu(fileName = "New Stat Upgrade List", menuName = "Data/Stats/Stat Upgrade List")]
    public class StatUpgradeList : ScriptableObject
    {
        private StatModType ModType = StatModType.Flat;
        [SerializeField] private AllCharacterStats Stat;
        [SerializeField] private List<UpgradeRarity> upgradeRarities = new();
                                  
        private float[] probabilities;

        public AllCharacterStats CharacterStat => Stat;

        public void Init()
        {
            probabilities = new float[upgradeRarities.Count];

            for (int i = 0; i < upgradeRarities.Count; i++)
            {
                probabilities[i] = upgradeRarities[i].Rarity.Chance / 100;
            }
        }

        public StatModifier GetModifier()
        {
            return new(ChooseRandomUpgradeStrength(), ModType);
        }

        private float ChooseRandomUpgradeStrength()
        {
            float randomValue = UnityEngine.Random.value;

            float cumulativeProbability = 0f;

            float strength = 10;

            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulativeProbability += probabilities[i];
                if (randomValue < cumulativeProbability)
                {
                    strength = upgradeRarities[i].Strength;
                    break;
                }
            }

            return strength;
        }
    }
}
