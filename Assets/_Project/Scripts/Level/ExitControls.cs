using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class ExitControls : MonoBehaviour {
        public GameObject LockedExit;
        public GameObject UnlockedExit;

        public GameObject ShinyExit;

        private void OnEnable()
        {
            PlayerRelatedEvents.PlayerClearedTheRoom += UnlockExit;
        }

        private void OnDisable()
        {
            PlayerRelatedEvents.PlayerClearedTheRoom -= UnlockExit;
        }


        public void UnlockExit()
        {
            LockedExit.SetActive(false);
            UnlockedExit.SetActive(true);
            ShinyExit.SetActive(true);
        }
    }
}