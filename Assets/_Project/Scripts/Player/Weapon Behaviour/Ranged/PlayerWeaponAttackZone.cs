using BadJuja.Player.Weapons;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttackZone {
    private List<Transform> _enemyTransforms = new();
    private Transform _weaponTransform;
    private float _radius;

    public bool HasEnemiesInReach {
        get {
            if (_enemyTransforms.Count > 0) return true;

            Collider[] cols = Physics.OverlapSphere(_weaponTransform.position, _radius, LayerMask.GetMask("Enemy"));
            if (cols.Length > 0)
            {
                foreach (Collider col in cols)
                {
                    _enemyTransforms.Add(col.transform);
                }
                return true;
            }
            else
                return false;
        }
    }

    public PlayerWeaponAttackZone(Transform weaponTransform, PlayerWeaponRanged weaponScript, float radius)
    {
        _weaponTransform = weaponTransform;
        _radius = radius;
        _weaponTransform.localScale = new(_radius * 2, 1, _radius * 2);

        weaponScript.EnemyEnteredRange += OnTriggerEnter;
        weaponScript.EnemyExitedRange += OnTriggerExit;
    }

    private void CleanList()
    {
       _enemyTransforms.RemoveAll(e => e == null);
    }
    private void OnTriggerEnter(Transform other)
    {
        _enemyTransforms.Add(other);
    }

    private void OnTriggerExit(Transform other)
    {
        _enemyTransforms.Remove(other);
    }

    public Transform GetClosestEnemy()
    {
        CleanList();

        Transform nearestTransform = null;
        float minDistance = float.MaxValue;

        if (HasEnemiesInReach)
        {
            foreach (Transform t in _enemyTransforms)
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
}
