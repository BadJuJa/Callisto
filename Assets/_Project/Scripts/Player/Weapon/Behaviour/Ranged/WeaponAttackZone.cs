using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.Weapons.Behaviour {
    public class WeaponAttackZone {
        public List<Transform> EnemyTransforms { get; private set; }
        private Transform _weaponTransform;
        private float _radius;

        public bool HasEnemiesInReach {
            get {
                CleanList();

                if (EnemyTransforms.Count > 0) return true;

                Collider[] cols = Physics.OverlapSphere(_weaponTransform.position, _radius, LayerMask.GetMask("Enemy"));
                if (cols.Length > 0)
                {
                    foreach (Collider col in cols)
                    {
                        EnemyTransforms.Add(col.transform);
                    }
                    return true;
                }
                else
                    return false;
            }
        }

        public WeaponAttackZone(Transform weaponTransform, WeaponRanged weaponScript, float radius)
        {
            EnemyTransforms = new();

            _weaponTransform = weaponTransform;
            _radius = radius;
            _weaponTransform.localScale = new(_radius * 2, 1, _radius * 2);

            weaponScript.EnemyEnteredRange += OnTriggerEnter;
            weaponScript.EnemyExitedRange += OnTriggerExit;
        }

        private void CleanList()
        {
            EnemyTransforms.RemoveAll(e => e == null);
        }
        private void OnTriggerEnter(Transform other)
        {
            EnemyTransforms.Add(other);
        }

        private void OnTriggerExit(Transform other)
        {
            EnemyTransforms.Remove(other);
        }

        public Transform GetClosestEnemy()
        {
            CleanList();

            Transform nearestTransform = null;
            float minDistance = float.MaxValue;

            if (HasEnemiesInReach)
            {
                foreach (Transform t in EnemyTransforms)
                {
                    if (t == null) continue;

                    float distance = Vector3.Distance(_weaponTransform.position, t.position);

                    if (distance < minDistance)
                    {
                        nearestTransform = t;
                        minDistance = distance;
                    }
                }
            }

            return nearestTransform;
        }

        public bool IsInRange(Transform t)
        {
            return Vector3.Distance(t.position, _weaponTransform.position) <= _radius;
        }
    }
}