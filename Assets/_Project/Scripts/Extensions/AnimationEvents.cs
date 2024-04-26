using BadJuja.Core.Events;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void AttackEvent()
    {
        PlayerRelatedEvents.Send_OnPlayerAttack();
    }

    public void AttackEnded()
    {
        PlayerRelatedEvents.Send_OnPlayerAttackEnded();
    }

    public void DisableVFX()
    {
        PlayerRelatedEvents.Send_OnPlayerAttackDisableVFX();
    }

    public void PrepareAttack()
    {
        PlayerRelatedEvents.Send_OnPlayerAttackPrepare();
    }

    public void FadedIn()
    {
        LevelLoadingRelatedEvents.Send_OnScreenFadeInFinished();
    }

    public void FadedOut()
    {
        LevelLoadingRelatedEvents.Send_OnScreenFadeOutFinished();
    }
}
