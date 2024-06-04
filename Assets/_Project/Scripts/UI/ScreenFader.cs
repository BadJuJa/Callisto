using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class ScreenFader : MonoBehaviour {
        public Animator animator;

        public float TimeToFadeOut;
        public float TimeToFadeIn;

        private void Awake()
        {
            if (animator == null) animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (animator == null) animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            LevelLoadingRelatedEvents.OnScreenFadeInStarted += FadeIn;
            LevelLoadingRelatedEvents.OnScreenFadeOutStarted += FadeOut;
        }

        private void OnDestroy()
        {
            LevelLoadingRelatedEvents.OnScreenFadeInStarted -= FadeIn;
            LevelLoadingRelatedEvents.OnScreenFadeOutStarted -= FadeOut;
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

        public void SendFadedIn()
        {
            LevelLoadingRelatedEvents.Send_OnScreenFadeInFinished();
        }
        
        public void SendFadedOut()
        {
            LevelLoadingRelatedEvents.Send_OnScreenFadeOutFinished();
        }

    }
}