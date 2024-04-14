using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Player {
    [RequireComponent(typeof(InputHandler), typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour {

        [Header("References")]
        public Transform ModelTransform;
        private CharacterController controller;
        private InputHandler _input;

        [Header("Parameters")]
        [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private float _turnSpeed = 360;

        [Header("Inner usage")]
        private Vector3 playerVelocity;
        private float _correctSpeed;
        private bool groundedPlayer;


        private void Awake()
        {
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
            ModelTransform.position = transform.position;
        }

        private void Move()
        {
            if (_input.MoveInputVector == Vector2.zero) return;

            controller.Move(Time.deltaTime * _correctSpeed * _input.MoveInputVector3D.ToRotation());
        }

        private void Look()
        {
            if (_input.LookInputVector == Vector2.zero && _input.MoveInputVector == Vector2.zero) return;

            var desiredInput = _input.LookInputVector == Vector2.zero ? _input.MoveInputVector3D.ToRotation() : _input.LookInputVector3D.ToRotation();

            var relative = transform.position + desiredInput - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            if (!ModelTransform) return;
            ModelTransform.rotation = Quaternion.RotateTowards(ModelTransform.rotation, rot, _turnSpeed * Time.deltaTime);
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