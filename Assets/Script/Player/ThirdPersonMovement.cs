using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController _controller;
    private PlayerAnimationController _playerAnim;

    [SerializeField]
    private Transform _cam;

    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _maxSpeed = 6f;
    [SerializeField]
    private float _minSpeed = 2f;
    private float _accelerationSpeed = 4f;
    private float _decelerationSpeed = 4f;

    [SerializeField]
    private float _turnSmoothSpeed = 0.1f;
    private float _turnSmoothVelocity;

    private Vector3 _velocity;
    private float _gravity = -19.6f;
    private float _jumpHeight = 2f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerAnim = GetComponent<PlayerAnimationController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        HandleMovementAndRotation(horizontal, vertical);
        Jump();
        HandleGravity();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    void HandleGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void HandleMovementAndRotation(float horizontal, float vertical)
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            WalkOrRunningSpeed();

            _controller.Move(moveDirection.normalized * _speed * Time.deltaTime);
        }
    }

    void WalkOrRunningSpeed()
    {
        if (_playerAnim.IsRunning && _speed < _maxSpeed)
        {
            _speed += _accelerationSpeed * Time.deltaTime;
        }
        
        if (!_playerAnim.IsRunning && _speed > _minSpeed)
        {
            if(_speed < _minSpeed)
            {
                _speed = _minSpeed;
            }
            else
            {
                _speed -= _decelerationSpeed * Time.deltaTime;
            }
        }
    }
}
