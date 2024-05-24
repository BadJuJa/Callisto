using BadJuja.Core;
using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Player.Visuals {
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour {
        private InputHandler _inputHandler;

        private Animator _animator;

        private Vector2 _animationDirection = new();

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _inputHandler = GameManager.InputHandler;
            GameRelatedEvents.OnPause += ToggleAnimator;
        }

        private void OnDestroy()
        {
            GameRelatedEvents.OnPause -= ToggleAnimator;
        }

        private void ToggleAnimator()
        {
            _animator.speed = _animator.speed == 0 ? 1 : 0;
        }

        private void Update()
        {
            if (!_inputHandler.IsEnabled) return;

            _animationDirection.x = 0f;
            _animationDirection.y = 1f;

            _animationDirection *= _inputHandler.MoveInputVector.magnitude;
            _animator.SetFloat("Move Input X", _animationDirection.x);
            _animator.SetFloat("Move Input Z", _animationDirection.y);

        }
    }
}