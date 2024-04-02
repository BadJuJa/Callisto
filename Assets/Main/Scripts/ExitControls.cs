using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitControls : MonoBehaviour
{
    public GameObject LockedExit;
    public GameObject UnlockedExit;

    public GameObject ShinyExit;

    public void UnlockExit()
    {
        LockedExit.SetActive(false);
        UnlockedExit.SetActive(true);
        ShinyExit.SetActive(true);
    }
}
