using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Player {
    public class OffensiveProjectile : MonoBehaviour {
        private ProjectileData _projectileData;

        private int _remainingPiercing;

        private float _damage;

        public void Initialize(ProjectileData projectileData, float damage)
        {
            _projectileData = projectileData;
            _damage = damage;

            _remainingPiercing = _projectileData.MaxPiercing;
        }

        private void Start()
        {
            var _rb = GetComponent<Rigidbody>();
            _rb.velocity = transform.forward * _projectileData.Speed;
            Destroy(gameObject, 5);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;

            if (other.TryGetComponent(out IDamagable damagable) && _remainingPiercing > 0)
            {
                damagable.TakeDamage(_damage);
                if (--_remainingPiercing < 1)
                    Destroy(gameObject);
            }
        }
    }
}