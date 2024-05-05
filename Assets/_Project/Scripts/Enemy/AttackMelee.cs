using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Enemy {
    public class AttackMelee : AttackBase {

        private LayerMask _playerLayer;

        public AttackMelee(MonoBehaviour context, IEnemyCentral enemyCentral) : base(context, enemyCentral)
        {
            _playerLayer = LayerMask.GetMask("Player");
        }

        public override void Attack()
        {
            Collider[] res = Physics.OverlapSphere(_attackPoint.position, .75f, _playerLayer);
            if (res.Length > 0)
            {
                if (res[0].TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(CalculateDamage());
                }
            }
            base.Attack();
        }
    }
}
