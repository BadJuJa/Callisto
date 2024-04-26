using BadJuja.Core.CharacterStats;
using BadJuja.Core;
using UnityEngine;
using System.Collections.Generic;

namespace BadJuja.Player {
    public class SelfUpgrade : MonoBehaviour {
        public AllCharacterStats Stat;
        public StatModType ModType;
        public float Strenght;

        public List<StatModifier> statModifiers = new();

        public virtual void Apply(Player player)
        {
            player.ApplyStatMod(Stat, ModType, Strenght, this);
        }
        public virtual void Remove(Player player)
        {
            player.RemoveStatMod(Stat, this);
        }

    }
}