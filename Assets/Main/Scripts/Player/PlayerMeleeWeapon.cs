using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerMeleeWeapon : MonoBehaviour {
        public Player _player;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;

            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_player.GetDamage);
            }
        }

    }
}