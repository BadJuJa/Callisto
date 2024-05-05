using UnityEngine;
using UnityEngine.InputSystem;
using BadJuja.Core.Events;

namespace BadJuja.Player {
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour {
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        //private InputAction _lookAction;
        private InputAction _switchToMeleeAction;
        private InputAction _switchToLightAction;
        private InputAction _switchToHeavyAction;

        private InputAction _pauseMenuAction;

        public Vector2 MoveInputVector => _moveAction.ReadValue<Vector2>();
        public Vector3 MoveInputVector3D => new Vector3(MoveInputVector.x, 0, MoveInputVector.y);

        //public Vector2 LookInputVector => _lookAction.ReadValue<Vector2>();
        //public Vector3 LookInputVector3D => new Vector3(LookInputVector.x, 0, LookInputVector.y);

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
            //_lookAction = _playerInput.actions["Look"];
            _switchToMeleeAction = _playerInput.actions["SwitchToMelee"];
            _switchToLightAction = _playerInput.actions["SwitchToLight"];
            _switchToHeavyAction = _playerInput.actions["SwitchToHeavy"];
            _pauseMenuAction = _playerInput.actions["PauseMenu"];


        }

        private void OnEnable()
        {
            _switchToMeleeAction.performed += ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(0);
            _switchToLightAction.performed += ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(1);
            _switchToHeavyAction.performed += ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(2);

            _pauseMenuAction.performed += ctx => GameRelatedEvents.Send_OnPause();

            _playerInput.actions["DEV.AddLevel"].performed += ctx => PlayerRelatedEvents.OnLevelIncrease();
        }

        private void OnDisable()
        {
            _switchToMeleeAction.performed -= ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(0);
            _switchToLightAction.performed -= ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(1);
            _switchToHeavyAction.performed -= ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(2);

            _pauseMenuAction.performed -= ctx => GameRelatedEvents.Send_OnPause();

            _playerInput.actions["DEV.AddLevel"].performed -= ctx => PlayerRelatedEvents.OnLevelIncrease();
        }
    }
}