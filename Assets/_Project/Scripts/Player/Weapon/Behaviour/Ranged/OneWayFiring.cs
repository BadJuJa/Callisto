using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.Weapons.Behaviour {

    public class OneWayFiring : IRangedWeaponBehaviour {

        private Transform _firingPointTransform;

        private Vector3 _firingDirection = Vector3.forward;
        
        public OneWayFiring(Transform firingPointTransform, Vector3 firingDirection)
        {
            _firingPointTransform = firingPointTransform;
            _firingDirection = firingDirection;
        }

        public Quaternion GetFiringDirection()
        {
            int mult = 1;
            if (_firingDirection == Vector3.back)
                mult = -1;

            return Quaternion.LookRotation(mult * _firingPointTransform.forward);
        }

        public Vector3 GetFiringPoint()
        {
            Vector3 offset = _firingDirection == Vector3.back ? _firingPointTransform.forward * -2 : Vector3.zero;
            return _firingPointTransform.position + offset;
        }

        public void Upgrade(Dictionary<string, float> args)
        {
        }
    }
}
