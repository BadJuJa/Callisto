using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class ShinyExitTrigger : MonoBehaviour {
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                PlayerRelatedEvents.Send_OnPlayerExitedRoom();
            }
        }
    }
}