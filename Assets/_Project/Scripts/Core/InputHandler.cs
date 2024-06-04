using BadJuja.Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BadJuja.Core {
    public class InputHandler {
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _switchToMeleeAction;
        private InputAction _switchToLightAction;
        private InputAction _switchToHeavyAction;

        private InputAction _pauseMenuAction;

        public Vector2 MoveInputVector => IsEnabled ? _moveAction.ReadValue<Vector2>() : Vector2.zero;

        public bool IsEnabled { get; private set; }

        public InputHandler(PlayerInput playerInputComponent)
        {
            _playerInput = playerInputComponent;
            _moveAction = _playerInput.actions["Move"];
            _switchToMeleeAction = _playerInput.actions["SwitchToMelee"];
            _switchToLightAction = _playerInput.actions["SwitchToLight"];
            _switchToHeavyAction = _playerInput.actions["SwitchToHeavy"];
            _pauseMenuAction = _playerInput.actions["PauseMenu"];

            EnablePauseAction();

            Enable();
        }

        public void SetActive(bool value)
        {
            if (value)
                Enable();
            else
                Disable();
        }

        private void Enable()
        {
            _switchToMeleeAction.performed += ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(0);
            _switchToLightAction.performed += ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(1);
            _switchToHeavyAction.performed += ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(2);

            _playerInput.actions["DEV.AddLevel"].performed += ctx => PlayerRelatedEvents.OnLevelIncrease();

            IsEnabled = true;
        }

        private void Disable()
        {
            _switchToMeleeAction.performed -= ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(0);
            _switchToLightAction.performed -= ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(1);
            _switchToHeavyAction.performed -= ctx => PlayerRelatedEvents.Send_OnSwitchToAttackType(2);

            _playerInput.actions["DEV.AddLevel"].performed -= ctx => PlayerRelatedEvents.OnLevelIncrease();

            IsEnabled = false;
        }

        public void DisablePauseAction()
        {
            _pauseMenuAction.performed -= ctx => GameRelatedEvents.Send_OnPause();
        }

        public void EnablePauseAction()
        {
            _pauseMenuAction.performed += ctx => GameRelatedEvents.Send_OnPause();
        }
    }
}