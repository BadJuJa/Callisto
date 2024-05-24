using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Player {
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour {

        [Header("References")]
        private Transform _modelTransform;
        private CharacterController _controller;
        private InputHandler _input;

        [Header("Parameters")]
        [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private float _turnSpeed = 360;

        [Header("Inner usage")]
        private Vector3 playerVelocity;
        private float _correctSpeed;
        private bool groundedPlayer;

        public void Initialize(Transform modelTransform)
        {
            _modelTransform = modelTransform;
            _input = GameManager.InputHandler;
            _controller = gameObject.GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_input.MoveInputVector != Vector2.zero)
            {
                _correctSpeed = _movementSpeed / _input.MoveInputVector.ToZXVector3().magnitude;
            }

            CheckGrounded();
            Move();
            ApplyGravity();

            Look();
        }

        private void LateUpdate()
        {
            _modelTransform.position = transform.position;
        }

        private void Move()
        {
            if (_input.MoveInputVector == Vector2.zero) return;

            _controller.Move(Time.deltaTime * _correctSpeed * _input.MoveInputVector.ToZXVector3().ToRotation());
        }

        private void Look()
        {
            if (_input.MoveInputVector == Vector2.zero) return;

            var desiredInput = _input.MoveInputVector.ToZXVector3().ToRotation();

            var relative = transform.position + desiredInput - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            if (!_modelTransform) return;
            _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, rot, _turnSpeed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            _controller.Move(playerVelocity * Time.deltaTime);
        }

        private void CheckGrounded()
        {
            groundedPlayer = _controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
        }
    }
}