using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using UnityEngine;

namespace BadJuja.Player.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon {
        
        protected WeaponElements _element;
        protected IStats _playerStats;

        private float Damage => _playerStats.GetStatValue(AllCharacterStats.Damage);

        public virtual void Initiate(WeaponElements element, IStats playerStats)
        {
            _element = element;

            _playerStats = playerStats;

            InitiateBehaviour();
        }

        public void DealDamage(IDamagable targetDamagable, int enemyCount = 1)
        {
            float finalDamage = GetFinalDamage() / enemyCount;
            targetDamagable.TakeDamage(finalDamage, _element);
        }

        private float GetElementalBonus() => _playerStats.GetElementalBonus(_element) / 100;

        private float GetCritMult()
        {
            if (Random.value < _playerStats.GetStatValue(AllCharacterStats.CritChance) / 100)
                return _playerStats.GetStatValue(AllCharacterStats.CritDamage) / 100;
            return 1;
        }
        
        private float GetFinalDamage()
        {
            float elementalMult = GetElementalBonus();
            float critMult = GetCritMult();

            return Damage * (1 + elementalMult) * critMult;
        }

        protected virtual void InitiateBehaviour()
        {
        }
    }
}
