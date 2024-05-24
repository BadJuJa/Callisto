using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class ExitControls : MonoBehaviour {

        [SerializeField] private GameObject[] ObjectsToEnable;
        [SerializeField] private GameObject[] ObjectsToDisable;

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
            foreach (GameObject item in ObjectsToEnable) item.SetActive(true);
            foreach (GameObject item in ObjectsToDisable) item.SetActive(false);
        }
    }
}