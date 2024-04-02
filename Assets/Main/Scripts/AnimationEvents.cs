using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void AttackEvent()
    {
        GlobalEvents.Send_OnPlayerAttack();
    }

    public void FadedIn()
    {
        GlobalEvents.Send_OnScreenFadeInFinished();
    }

    public void FadedOut()
    {
        GlobalEvents.Send_OnScreenFadeOutFinished();
    }
}
