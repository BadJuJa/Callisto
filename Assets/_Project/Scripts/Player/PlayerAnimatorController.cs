using UnityEngine;

namespace BadJuja.Player {
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorController : MonoBehaviour {
        [SerializeField] private InputHandler _inputHandler;

        private Animator _animator;

        private Vector2 _animationDirection = new();

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _inputHandler = GetComponentInParent<InputHandler>();
        }

        private void Update()
        {
            if (!_inputHandler) return;

            _animationDirection.x = 0f;
            _animationDirection.y = 1f;

            _animationDirection *= _inputHandler.MoveInputVector.magnitude;
            _animator.SetFloat("Move Input X", _animationDirection.x);
            _animator.SetFloat("Move Input Z", _animationDirection.y);

        }
    }
}