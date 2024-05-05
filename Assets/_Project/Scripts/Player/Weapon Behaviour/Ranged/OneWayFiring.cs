using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.Weapons.Behaviour {

    public class OneWayFiring : IRangedWeaponBehaviour {

        private Transform _firingPointTransform;

        private Vector3 _firingDirection = Vector3.forward;

        private Vector3 FiringPoint {
            get {
                return _firingPointTransform.position + Offset;
            }
        }
        private Vector3 Offset {
            get {
                return _firingDirection == Vector3.back ? _firingPointTransform.forward * -2 : Vector3.zero;
            }
        }
        private Quaternion FiringDirection {
            get {
                int mult = 1;
                if (_firingDirection == Vector3.back)
                    mult = -1;

                return Quaternion.LookRotation(mult * _firingPointTransform.forward);
            }
        }

        public OneWayFiring(Transform firingPointTransform, Vector3 firingDirection)
        {
            _firingPointTransform = firingPointTransform;
            _firingDirection = firingDirection;
        }

        public Quaternion GetFiringDirection()
        {
            return FiringDirection;
        }

        public Vector3 GetFiringPoint()
        {
            return FiringPoint;
        }

        public void Upgrade(Dictionary<string, float> args)
        {
        }
    }
}
