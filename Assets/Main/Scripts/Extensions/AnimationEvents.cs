using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void AttackEvent()
    {
        GlobalEvents.Send_OnPlayerAttack();
    }

    public void AttackEnded()
    {
        GlobalEvents.Send_OnPlayerAttackEnded();
    }

    public void DisableVFX()
    {
        GlobalEvents.Send_OnPlayerAttackDisableVFX();
    }

    public void PrepareAttack()
    {
        GlobalEvents.Send_OnPlayerAttackPrepare();
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
