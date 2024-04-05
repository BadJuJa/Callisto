using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public Transform ModelTransform;

    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _turnSpeed = 360;

    private InputHandler _input;
    private Rigidbody _rb;


    private Vector3 _finalMovementVector;


    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        _rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (_input.MoveInputVector != Vector2.zero)
        {
            float correct_speed = _movementSpeed / _input.MoveInputVector3D.magnitude * Time.deltaTime;
            _finalMovementVector = transform.position + correct_speed * _input.MoveInputVector3D.ToRotation();
        }
        
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_input.MoveInputVector == Vector2.zero) return;

        
        _rb.MovePosition(_finalMovementVector);
    }

    private void Look()
    {
        if (_input.LookInputVector == Vector2.zero && _input.MoveInputVector == Vector2.zero) return;
        
        var desiredInput = _input.LookInputVector == Vector2.zero ? _input.MoveInputVector3D.ToRotation() : _input.LookInputVector3D.ToRotation();
        
        var relative = (transform.position + desiredInput) - transform.position;
        var rot = Quaternion.LookRotation(relative, Vector3.up);

        if (!ModelTransform) return;
        ModelTransform.rotation = Quaternion.RotateTowards(ModelTransform.rotation, rot, _turnSpeed * Time.deltaTime);
    }
}