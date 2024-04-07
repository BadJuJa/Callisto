using UnityEngine;

public class ShinyExitTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            GlobalEvents.Send_OnPlayerExitedRoom();
        }
    }
}
