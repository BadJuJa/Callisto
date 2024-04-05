using UnityEngine;

public class ExitControls : MonoBehaviour
{
    public GameObject LockedExit;
    public GameObject UnlockedExit;

    public GameObject ShinyExit;

    private void OnEnable()
    {
        GlobalEvents.OnPlayerClearedTheRoom += UnlockExit;
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerClearedTheRoom -= UnlockExit;
    }


    public void UnlockExit()
    {
        LockedExit.SetActive(false);
        UnlockedExit.SetActive(true);
        ShinyExit.SetActive(true);
    }
}
