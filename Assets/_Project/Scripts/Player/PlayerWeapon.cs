using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player
{
    public abstract class PlayerWeapon : MonoBehaviour, IWeapon {
        protected IWeaponBehaviour _iBehaviour;
        protected WeaponElements _element;

        protected int _damage;

        protected Dictionary<string, dynamic> _weaponParametersDict;

        public virtual void Initiate(WeaponElements element)
        {
            _element = element;

            InitiateBehaviour();
        }

        public void DealDamage(IDamagable targetDamagable)
        {
            float targetResistance = 0;
            switch (_element)
            {
                case WeaponElements.Fire:
                    targetResistance = targetDamagable.GetFireResistance;
                    break;
                case WeaponElements.Frost:
                    targetResistance = targetDamagable.GetFrostResistance;
                    break;
                case WeaponElements.Shock:
                    targetResistance = targetDamagable.GetShockResistance;
                    break;
            }
            targetDamagable.TakeDamage(_damage * (1 - targetResistance));
        }


        [ContextMenu("Upgrade Test")]
        public void UpdateStats()
        {
            //switch (_behaviour)
            //{
            //    case WeaponBehaviour.CircularMotion:
            //        Dictionary<string, dynamic> d = new()
            //        {
            //            { "ObjectCount", true },
            //            { "Speed", 2 }
            //        };
            //        _iBehaviour.Upgrade(d);
            //        break;
            //}
        }

        protected virtual void InitiateBehaviour()
        {
        }
    }
}
