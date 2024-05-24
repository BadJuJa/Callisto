using BadJuja.Core;
using BadJuja.Core.Events;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.Weapons.Behaviour {
    public class TargetNearestEnemy : IRangedWeaponBehaviour {

        private Transform _firingPointTransform;
        private WeaponAttackZone _attackZone;
        private Transform _priorityTarget;
        public TargetNearestEnemy(Transform firingPointTransform, WeaponAttackZone attackZoneScript)
        {
            _firingPointTransform = firingPointTransform;
            _attackZone = attackZoneScript;
            EnemyRelatedEvents.PriorityTargetChanged += (newTarget) => _priorityTarget = newTarget;
        }

        public Vector3 GetFiringPoint()
        {
            return _firingPointTransform.position;
        }

        public Quaternion GetFiringDirection()
        {
            Transform enemy;

            if (_priorityTarget != null && _attackZone.IsInRange(_priorityTarget))
                enemy = _priorityTarget;
            else
                enemy = _attackZone.GetClosestEnemy();
            
            if (enemy == null) return Quaternion.identity;

            return Quaternion.LookRotation(enemy.position - GetFiringPoint());
        }

        public void Upgrade(Dictionary<string, float> args)
        {
        }

    }
}