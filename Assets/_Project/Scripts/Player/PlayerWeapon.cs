using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using UnityEngine;

namespace BadJuja.Player.Weapons
{
    public abstract class PlayerWeapon : MonoBehaviour, IWeapon {
        
        protected WeaponElements _element;
        protected IStats _playerStats;

        protected float _damage => _playerStats.GetStatValue(AllCharacterStats.Damage);

        public virtual void Initiate(WeaponElements element, IStats playerStats)
        {
            _element = element;

            _playerStats = playerStats;

            InitiateBehaviour();
        }

        public void DealDamage(IDamagable targetDamagable, IStats targetStats, int enemyCount = 1)
        {
            float finalDamage = GetFinalDamage(targetStats) / enemyCount;
            targetDamagable.TakeDamage(finalDamage);
        }

        /// <summary>
        /// </summary>
        /// <param name="targetDamagable"></param>
        /// <returns>Float range from 0 to 1</returns>
        private float GetTargetResistance(IStats targetStats)
        {
            float targetResistance = 0;
            switch (_element)
            {
                case WeaponElements.Fire:
                    targetResistance = targetStats.GetStatValue(AllCharacterStats.FireResistance);
                    break;
                case WeaponElements.Frost:
                    targetResistance = targetStats.GetStatValue(AllCharacterStats.FrostResistance);
                    break;
                case WeaponElements.Shock:
                    targetResistance = targetStats.GetStatValue(AllCharacterStats.ShockResistance);
                    break;
            }
            return targetResistance / 100;
        }

        private float GetPlayerElementalBonus()
        {
            float elementalBonus = 0;
            switch (_element)
            {
                case WeaponElements.Fire:
                    elementalBonus = _playerStats.GetStatValue(AllCharacterStats.FireDamage);
                    break;
                case WeaponElements.Frost:
                    elementalBonus = _playerStats.GetStatValue(AllCharacterStats.FrostDamage);
                    break;
                case WeaponElements.Shock:
                    elementalBonus = _playerStats.GetStatValue(AllCharacterStats.ShockDamage);
                    break;
            }
            return elementalBonus / 100;
        }

        private float GetCritMult()
        {
            if (Random.value < _playerStats.GetStatValue(AllCharacterStats.CritChance) / 100)
                return _playerStats.GetStatValue(AllCharacterStats.CritDamage) / 100;
            return 1;
        }

        private float GetFinalDamage(IStats targetStats)
        {
            float elementalMult = GetPlayerElementalBonus();
            float targetResistance = GetTargetResistance(targetStats);
            float critMult = GetCritMult();

            return _damage * (1 + (elementalMult - targetResistance)) * critMult;
        }

        

        public void UpdateStats()
        {
        }

        protected virtual void InitiateBehaviour()
        {
        }
    }
}
