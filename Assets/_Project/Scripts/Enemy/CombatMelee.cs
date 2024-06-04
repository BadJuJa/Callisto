using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Enemy {
    public class CombatMelee : CombatBase {

        private LayerMask _playerLayer;

        public CombatMelee(MonoBehaviour context, IEnemyCentral enemyCentral) : base(context, enemyCentral)
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
                    damagable.TakeDamage(CalculateDamage(), _data.Element.Element);
                }
            }
            base.Attack();
        }
    }
}
