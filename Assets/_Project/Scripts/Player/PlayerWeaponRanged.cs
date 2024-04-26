using BadJuja.Core;
using BadJuja.Core.Data;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerWeaponRanged : PlayerWeapon {

        private RangedWeaponData _data;
        private RangedWeaponBehaviour _behaviour;

        public void Initiate(RangedWeaponData data, WeaponElements element)
        {
            _data = data;
            _behaviour = _data.Behaviour;

            _damage = _data.Damage;

            base.Initiate(element);
        }

        protected override void InitiateBehaviour()
        {
            base.InitiateBehaviour();
            _weaponParametersDict = new()
            {
                { "Prefab", _data.Model },
                { "ParentTransform", gameObject.transform },
                { "Radius", _data.Radius },
                { "FiringPointTransform", GetComponentInParent<PlayerCombat>().FiringPoint },
                { "Angle", 2f },
                { "FireRate", _data.FireRate }
            };
            

            switch (_behaviour)
            {
                case RangedWeaponBehaviour.FiringForward:
                    _iBehaviour = gameObject.AddComponent<OneWayFiring>().GetComponent<IWeaponBehaviour>();
                    _weaponParametersDict.Add("Direction", Vector3.forward);
                    break;
                case RangedWeaponBehaviour.FiringBackward:
                    _iBehaviour = gameObject.AddComponent<OneWayFiring>().GetComponent<IWeaponBehaviour>();
                    _weaponParametersDict.Add("Direction", Vector3.back);
                    break;
                case RangedWeaponBehaviour.NearestTarget:
                    break;
            }
            
            _iBehaviour.Init(_weaponParametersDict);
        }
    }
}