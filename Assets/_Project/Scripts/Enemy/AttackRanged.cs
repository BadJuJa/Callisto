using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using UnityEngine;

namespace BadJuja.Enemy {
    public class AttackRanged : AttackBase {

        protected CharacterController _playerCC;
        protected Transform _parentTransform;
        protected Projectile _enemyProjectileData;
        protected float _spreadAngle = 7.5f;

        public AttackRanged(Transform parentTransform, MonoBehaviour context, IEnemyCentral enemyCentral) : base(context, enemyCentral)
        {
            _parentTransform = parentTransform;
            _playerCC = _centralInterface.PlayerTransform.gameObject.GetComponent<CharacterController>();
        }

        public override void Initialize(EnemyData Data, IStats stats, Transform attackPoint, AnimatorControllerScript animatorController)
        {
            base.Initialize(Data, stats, attackPoint, animatorController);
            _enemyProjectileData = _data.AttackProjectilePrefab.GetComponent<Projectile>();
        }

        public override void Attack()
        {
            Vector3 targetPosition = CalculateInterceptPosition(
                        _centralInterface.PlayerTransform.position,
                        _playerCC.velocity,
                        _parentTransform.position,
                        _enemyProjectileData.ProjectileData.Speed);
            
            Vector3 direction = targetPosition - _parentTransform.position;

            float spread = Random.Range(-_spreadAngle, _spreadAngle);
            direction = Quaternion.Euler(0, spread, 0) * direction;
            direction.Normalize();

            _parentTransform.LookAt(targetPosition);

            Object.Instantiate(
                _data.AttackProjectilePrefab,
                _attackPoint.position,
                Quaternion.LookRotation(_attackPoint.forward),
                _attackPoint
            ).GetComponent<Projectile>().Initialize(CalculateDamage());
            
            base.Attack();
        }

        private Vector3 CalculateInterceptPosition(Vector3 targetPosition, Vector3 targetVelocity, Vector3 shooterPosition, float bulletSpeed)
        {
            Vector3 relativePosition = targetPosition - shooterPosition;

            float distance = relativePosition.magnitude;

            float timeToTarget = distance / bulletSpeed;

            Vector3 interceptPosition = targetPosition + targetVelocity * timeToTarget;

            return interceptPosition;
        }
    }
}
