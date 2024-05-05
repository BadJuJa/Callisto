using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using System;
using System.Collections;
using BadJuja.Player.Weapons.Behaviour;
using UnityEngine;

namespace BadJuja.Player.Weapons {
    public class PlayerWeaponRanged : PlayerWeapon {

        private RangedWeaponData _data;
        private RangedWeaponBehaviour _behaviour;
        private IRangedWeaponBehaviour _iBehaviour;
        private IWeapon _iWeapon;

        private PlayerWeaponAttackZone _attackZoneScript;

        public Action<Transform> EnemyEnteredRange;
        public Action<Transform> EnemyExitedRange;

        private float _fireRate = 1f;
        private bool _canShoot = false;

        private void OnEnable()
        {
            _canShoot = true;
        }

        public void Initiate(RangedWeaponData data, WeaponElements element, IStats playerStats)
        {
            _data = data;
            _iWeapon = GetComponent<IWeapon>();

            _attackZoneScript = new(transform, this, _data.Radius);

            _behaviour = _data.Behaviour;
            _fireRate = _data.FireRate;

            base.Initiate(element, playerStats);

            _canShoot = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyEnteredRange?.Invoke(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            EnemyExitedRange?.Invoke(other.transform);
        }

        protected override void InitiateBehaviour()
        {
            base.InitiateBehaviour();

            switch (_behaviour)
            {
                case RangedWeaponBehaviour.FiringForward:
                    _iBehaviour = new OneWayFiring(GetComponentInParent<PlayerCombat>().FiringPoint, Vector3.forward);
                    break;
                case RangedWeaponBehaviour.FiringBackward:
                    _iBehaviour = new OneWayFiring(GetComponentInParent<PlayerCombat>().FiringPoint, Vector3.back);                    
                    break;
                case RangedWeaponBehaviour.NearestTarget:
                    _iBehaviour = new TargetNearestEnemy(gameObject.transform, _attackZoneScript);
                    break;
            }
        }

        private void Update()
        {
            if (_canShoot) Shoot();
        }

        public bool ReadyToShoot()
        {
            return _attackZoneScript.HasEnemiesInReach;
        }

        private void Shoot()
        {
            if (!ReadyToShoot())
            {
                StartCoroutine(RespectFireRate());
                return;
            }

            var rot = _iBehaviour.GetFiringDirection();

            if (rot != Quaternion.identity)
            {
                var projectile = Instantiate(_data.Model, _iBehaviour.GetFiringPoint(), rot, transform);
                projectile.transform.localScale = projectile.transform.localScale / 10;
                projectile.GetComponent<OffensiveProjectile>().Initialize(_iWeapon);
            }

            StartCoroutine(RespectFireRate());
        }

        private IEnumerator RespectFireRate()
        {
            _canShoot = false;
            yield return new WaitForSeconds(1f / _fireRate);
            _canShoot = true;
        }
    }
}