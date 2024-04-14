using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerWeaponHolder : MonoBehaviour {
        public GameObject Weapon;
        public GameObject VFX;

        private void OnEnable()
        {
            GlobalEvents.OnPlayerAttacked += AttackEvent;
            GlobalEvents.OnPlayerAttackEnded += AttackEnded;
            GlobalEvents.OnPlayerAttackPrepare += PrepareAttack;
            GlobalEvents.OnPlayerAttackDisableVFX += DisableVFX;
        }

        private void OnDisable()
        {
            GlobalEvents.OnPlayerAttacked -= AttackEvent;
            GlobalEvents.OnPlayerAttackEnded -= AttackEnded;
            GlobalEvents.OnPlayerAttackPrepare -= PrepareAttack;
            GlobalEvents.OnPlayerAttackDisableVFX -= DisableVFX;
        }
        public void PrepareAttack()
        {
            Weapon.SetActive(true);
        }

        public void AttackEvent()
        {
            VFX.SetActive(true);
        }
        public void DisableVFX()
        {
            VFX.SetActive(false);
        }

        public void AttackEnded()
        {
            Weapon.SetActive(false);
        }


    }
}