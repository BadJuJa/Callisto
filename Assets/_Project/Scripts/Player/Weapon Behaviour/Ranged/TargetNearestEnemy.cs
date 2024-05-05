using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.Weapons.Behaviour {
    public class TargetNearestEnemy : IRangedWeaponBehaviour {

        private Transform _firingPointTransform;
        private PlayerWeaponAttackZone _attackZone;

        public TargetNearestEnemy(Transform firingPointTransform, PlayerWeaponAttackZone attackZoneScript)
        {
            _firingPointTransform = firingPointTransform;
            _attackZone = attackZoneScript;
        }

        public Vector3 GetFiringPoint()
        {
            return _firingPointTransform.position;
        }

        public Quaternion GetFiringDirection()
        {
            Transform enemy = _attackZone.GetClosestEnemy();
            if (enemy == null) return Quaternion.identity;

            return Quaternion.LookRotation(enemy.position - GetFiringPoint());
        }

        public void Upgrade(Dictionary<string, float> args)
        {
        }

    }
}