using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerVisualsControl : MonoBehaviour {
        [Header("Input Reference")]
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Transform _modelTransform;
        [SerializeField] private SharedPlayerData _playerData;

        [Header("AttackIndicator")]
        [SerializeField] private Animator _attackIndicatorAnimator;
        [SerializeField] private float _attackIndicatorTimeToExtend = 1f;

        private void Awake()
        {
            _inputHandler = GetComponentInParent<InputHandler>();
            _attackIndicatorAnimator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            PlayerRelatedEvents.OnPlayerChangedAttackType += UpdateAttackFillTime;
            PlayerRelatedEvents.OnSwitchToAttackType += (value) => _attackIndicatorAnimator.SetInteger("AttackType", value);
        }

        private void OnDisable()
        {
            PlayerRelatedEvents.OnPlayerChangedAttackType -= UpdateAttackFillTime;
            PlayerRelatedEvents.OnSwitchToAttackType += (value) => _attackIndicatorAnimator.SetInteger("AttackType", value);
        }

        private void Update()
        {
            transform.rotation = _modelTransform.rotation;
            _attackIndicatorAnimator.SetBool("IsAttacking", _inputHandler.LookInputVector != Vector2.zero);
        }

        private void UpdateAnimationLengths()
        {
            if (_attackIndicatorAnimator != null)
                _attackIndicatorAnimator.speed = 1 / _attackIndicatorTimeToExtend;
        }

        private void UpdateAttackFillTime()
        {
            _attackIndicatorTimeToExtend = _playerData.CurrentAttackTime;
            UpdateAnimationLengths();
        }
    }
}