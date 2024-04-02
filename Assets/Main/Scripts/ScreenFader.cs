using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public Animator animator;

    public float TimeToFadeOut;
    public float TimeToFadeIn;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GlobalEvents.Event_OnScreenFadeInStarted += FadeIn;
        GlobalEvents.Event_OnScreenFadeOutStarted += FadeOut;
    }

    private void OnDisable()
    {
        GlobalEvents.Event_OnScreenFadeInStarted += FadeIn;
        GlobalEvents.Event_OnScreenFadeOutStarted += FadeOut;
    }

    private void FadeIn()
    {
        animator.speed = 1 / TimeToFadeIn;
        animator.SetTrigger("FadeIn");
    }
    private void FadeOut()
    {
        animator.speed = 1 / TimeToFadeOut;
        animator.SetTrigger("FadeOut");
    }
    
}
