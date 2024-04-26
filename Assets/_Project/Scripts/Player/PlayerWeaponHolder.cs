using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerWeaponHolder : MonoBehaviour {
        public GameObject Weapon;
        public GameObject VFX;

        private void OnEnable()
        {
            PlayerRelatedEvents.OnPlayerAttacked += AttackEvent;
            PlayerRelatedEvents.OnPlayerAttackEnded += AttackEnded;
            PlayerRelatedEvents.OnPlayerAttackPrepare += PrepareAttack;
            PlayerRelatedEvents.OnPlayerAttackDisableVFX += DisableVFX;
        }

        private void OnDisable()
        {
            PlayerRelatedEvents.OnPlayerAttacked -= AttackEvent;
            PlayerRelatedEvents.OnPlayerAttackEnded -= AttackEnded;
            PlayerRelatedEvents.OnPlayerAttackPrepare -= PrepareAttack;
            PlayerRelatedEvents.OnPlayerAttackDisableVFX -= DisableVFX;
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