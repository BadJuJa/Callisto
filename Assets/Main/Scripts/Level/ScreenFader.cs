using BadJuja.Core;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class ScreenFader : MonoBehaviour {
        public Animator animator;

        public float TimeToFadeOut;
        public float TimeToFadeIn;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GlobalEvents.OnScreenFadeInStarted += FadeIn;
            GlobalEvents.OnScreenFadeOutStarted += FadeOut;
        }

        private void OnDisable()
        {
            GlobalEvents.OnScreenFadeInStarted += FadeIn;
            GlobalEvents.OnScreenFadeOutStarted += FadeOut;
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
}