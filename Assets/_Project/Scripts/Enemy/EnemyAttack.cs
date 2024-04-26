using BadJuja.Core;
using BadJuja.Core.Data;
using UnityEngine;

namespace BadJuja.Enemy {
    public class EnemyAttack : MonoBehaviour {
        [Header("Combat")]
        public float SpredAngle = 0;
        private float _attackCooldown;
        private bool _canAttack = false;

        [Header("References")]
        private EnemyAnimatorController _animatorController;
        private IEnemyCentral _centralInterface;
        public Transform AttackPoint;
        public LayerMask PlayerLayer;
        private CharacterController _playerCC;
        private EnemyData _data;
        private EnemyProjectile _enemyProjectileData;
        private bool _localInit = false;

        private void Awake()
        {
            _centralInterface = GetComponentInParent<IEnemyCentral>();
        }

        private void Start()
        {
            _animatorController = GetComponentInChildren<EnemyAnimatorController>();
            _playerCC = _centralInterface.PlayerTransform.gameObject.GetComponent<CharacterController>();
        }


        private void Update()
        {
            if (!_localInit) return;

            if (_animatorController == null) return;

            if (!_canAttack)
            {
                if (_attackCooldown <= 0) _canAttack = true;
                else _attackCooldown -= Time.deltaTime;
            }

            if (_canAttack && _centralInterface.PlayerInReachDistance)
            {
                if (_animatorController)
                    _animatorController.TriggerAttack();
                _canAttack = false;
                _attackCooldown = _data.AttackCooldown;
            }
        }

        public void Attack()
        {
            switch (_data.EnemyType)
            {
                case EnemyTypes.Melee:
                    Collider[] res = Physics.OverlapSphere(AttackPoint.position, .75f, PlayerLayer);
                    if (res.Length > 0)
                    {
                        if (res[0].TryGetComponent(out IDamagable damagable))
                        {
                            damagable.TakeDamage(_data.Damage);
                        }
                    }

                    break;

                case EnemyTypes.Range:
                    Vector3 targetPosition = CalculateInterceptPosition(
                        _centralInterface.PlayerTransform.position,
                        _playerCC.velocity,
                        transform.position,
                        _enemyProjectileData.ProjectileData.Speed);

                    Vector3 direction = targetPosition - transform.position;

                    float spread = Random.Range(-SpredAngle, SpredAngle);
                    direction = Quaternion.Euler(0, spread, 0) * direction;
                    direction.Normalize();

                    transform.LookAt(targetPosition);

                    Instantiate(
                        _data.AttackProjectilePrefab,
                        AttackPoint.position,
                        Quaternion.LookRotation(AttackPoint.forward),
                        AttackPoint
                    ).GetComponent<EnemyProjectile>().Initialize(_data.Damage);

                    break;
            }

        }

        Vector3 CalculateInterceptPosition(Vector3 targetPosition, Vector3 targetVelocity, Vector3 shooterPosition, float bulletSpeed)
        {
            Vector3 relativePosition = targetPosition - shooterPosition;

            float distance = relativePosition.magnitude;

            float timeToTarget = distance / bulletSpeed;

            Vector3 interceptPosition = targetPosition + targetVelocity * timeToTarget;

            return interceptPosition;
        }
        public void Initialize(EnemyData Data)
        {
            _data = Data;
            _attackCooldown = _data.AttackCooldown;
            if (_data.AttackProjectilePrefab != null)
            {
                _enemyProjectileData = _data.AttackProjectilePrefab.GetComponent<EnemyProjectile>();
            }
            _localInit = true;
        }
    }
}