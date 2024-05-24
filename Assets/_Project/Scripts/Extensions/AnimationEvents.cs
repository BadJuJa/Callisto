using BadJuja.Core.Events;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void AttackEvent()
    {
        PlayerRelatedEvents.Send_OnAttack();
    }

    public void AttackEnded()
    {
        PlayerRelatedEvents.Send_OnAttackEnded();
    }

    public void DisableVFX()
    {
        PlayerRelatedEvents.Send_OnAttackDisableVFX();
    }

    public void PrepareAttack()
    {
        PlayerRelatedEvents.Send_OnAttackPrepare();
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
