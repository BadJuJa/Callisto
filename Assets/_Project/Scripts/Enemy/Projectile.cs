using BadJuja.Core;
using UnityEngine;
 
namespace BadJuja.Enemy {
    public class Projectile : MonoBehaviour {
        public ProjectileData ProjectileData;
        private float _damage;
        private WeaponElements _element;
        private int _remainingPiercing;

        public void Initialize(float damage, WeaponElements element)
        {
            _remainingPiercing = ProjectileData.MaxPiercing;
            _damage = damage;
            _element = element;

            var _rb = GetComponent<Rigidbody>();
            _rb.velocity = transform.forward * ProjectileData.Speed;

            Destroy(gameObject, 5);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            if (other.TryGetComponent(out IDamagable damagable) && _remainingPiercing > 0)
            {
                damagable.TakeDamage(_damage, _element);
                if (--_remainingPiercing < 1)
                    Destroy(gameObject);
            }
        }
    }
}