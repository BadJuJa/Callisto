using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using UnityEngine;

namespace BadJuja.Player {
    public class OffensiveProjectile : MonoBehaviour {
        
        private int _remainingPiercing = 1;
        IWeapon _iWeapon;

        public void Initialize(IWeapon iWeapon, float speed = 25f, float reach = 50f)
        {
            _iWeapon = iWeapon;
            
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            
            Destroy(gameObject, reach/speed);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;

            if (!other.CompareTag("Enemy")) return;

            if (other.TryGetComponent(out IDamagable damagable) && other.TryGetComponent(out IStats stats) && _remainingPiercing > 0)
            {
                _iWeapon.DealDamage(damagable, stats);
                if (--_remainingPiercing < 1)
                    Destroy(gameObject);
            }
        }
    }
}