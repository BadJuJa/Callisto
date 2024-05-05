using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Player {
    [RequireComponent(typeof(InputHandler), typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour {

        [Header("References")]
        private Transform _modelTransform;
        private CharacterController controller;
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
            _input = GetComponent<InputHandler>();
            controller = gameObject.GetComponent<CharacterController>();

        }

        private void Update()
        {
            if (_input.MoveInputVector != Vector2.zero)
            {
                _correctSpeed = _movementSpeed / _input.MoveInputVector3D.magnitude;
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

            controller.Move(Time.deltaTime * _correctSpeed * _input.MoveInputVector3D.ToRotation());
        }

        private void Look()
        {
            if (_input.MoveInputVector == Vector2.zero) return;

            var desiredInput = _input.MoveInputVector3D.ToRotation();

            var relative = transform.position + desiredInput - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            if (!_modelTransform) return;
            _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, rot, _turnSpeed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

        private void CheckGrounded()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
        }
    }
}