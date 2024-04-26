using BadJuja.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player {

    public class OneWayFiring : MonoBehaviour, IWeaponBehaviour {
        private GameObject _projectilePrefab;

        private Transform _parentTransform;
        private Transform _firingPointTransform;

        private Vector3 _firingDirection = Vector3.forward;
        private float _angle = 1f;
        private float _fireRate = 1f;
        private float _attackRadius = 1f;
        
        private Vector3 FiringPoint {
            get {
                return _firingPointTransform.position + Offset;
            }
        }
        private Vector3 Offset {
            get {
                if (_firingDirection == Vector3.back)
                    return _firingPointTransform.forward * -2;
                else return Vector3.zero;
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
        
        private bool _canShoot;

        public void Init(dynamic Args)
        {
            Dictionary<string, dynamic> _args;
            try
            {
                _args = Args;
            }
            catch (System.Exception e)
            {
                print(e);
                return;
            }

            if (_args.TryGetValue("Prefab", out dynamic prefab))
                _projectilePrefab = prefab;
            if (_args.TryGetValue("ParentTransform", out dynamic parentT))
                _parentTransform = parentT;
            if (_args.TryGetValue("FiringPointTransform", out dynamic firingPointTransform))
                _firingPointTransform = firingPointTransform;
            if (_args.TryGetValue("Direction", out dynamic direction))
                _firingDirection = direction;
            if (_args.TryGetValue("Angle", out dynamic angle))
                _angle = angle;
            if (_args.TryGetValue("FireRate", out dynamic fireRate))
                _fireRate = fireRate;
            if (_args.TryGetValue("Radius", out dynamic attackRadius))
                _attackRadius = attackRadius;
            
            _canShoot = true;
        }

        public void Upgrade(dynamic Args)
        {

        }

        private void Update()
        {
            if (_canShoot) Shoot();
        }

        private void Shoot()
        {
            var tcube = Instantiate(_projectilePrefab, FiringPoint, FiringDirection, transform);
            

        _canShoot = false;
        StartCoroutine(RespectFireRate());
        }

        private IEnumerator RespectFireRate()
        {
            yield return new WaitForSeconds(1f / _fireRate);
            _canShoot = true;
        }
    }
}
