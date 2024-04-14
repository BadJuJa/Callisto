using BadJuja.Core.CharacterStats;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core {
    public abstract class CharacterCore : MonoBehaviour {

        protected CharacterStat Health = new();
        protected float _currentHealth = 0;

        protected CharacterStat DamageReduction = new();
        protected CharacterStat Damage = new();

        protected Dictionary<AllCharacterStats, CharacterStat> Stats = new();

        protected virtual void Awake()
        {
            Stats.Add(AllCharacterStats.Health, Health);
            Stats.Add(AllCharacterStats.DamageReduction, DamageReduction);
            Stats.Add(AllCharacterStats.Damage, Damage);

            _currentHealth = Health.Value;
        }

        public virtual void TakeDamage(float value) { }
        protected virtual void Die() { }
    }
}