using BadJuja.Core;
using BadJuja.Core.Data;
using System.Collections.Generic;

namespace BadJuja.Player {
    public class PlayerWeaponMelee : PlayerWeapon {

        private MeleeWeaponData _data;
        private MeleeWeaponBehaviour _behaviour;

        public void Initiate(MeleeWeaponData data, WeaponElements element)
        {
            _data = data;
            _behaviour = _data.Behaviour;

            _damage = _data.Damage;

            base.Initiate(element);
        }

        protected override void InitiateBehaviour()
        {
            _weaponParametersDict = new()
            {
                { "Prefab", _data.Model },
                { "ParentTransform", gameObject.transform },
                { "Radius", _data.Radius },
            };

            switch (_behaviour)
            {
                case MeleeWeaponBehaviour.CircularMotion:
                    _iBehaviour = gameObject.AddComponent<CircleDistribution>().GetComponent<IWeaponBehaviour>();
                    _iBehaviour.Init(_weaponParametersDict);
                    break;
                case MeleeWeaponBehaviour.AreaDamage:
                    break;
            }
        }
    }
}