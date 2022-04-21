using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private AudioManager _audioManager;
    private int _hashVelocityZ;
    private float _maxWalkSpeed = 0.5f;
    private float _maxRunningSpeed = 2f;
    private float _acceleration = 2f;
    private float _deceleration = 2f;
    [SerializeField]
    private float _animationSpeed = 0f;
    private int _spellLayerIndex;

    public bool IsWalking { get; private set; }
    public bool IsRunning { get; private set; }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _hashVelocityZ = Animator.StringToHash("Velocity Z");
        _spellLayerIndex = _animator.GetLayerIndex("Spell Layer");
    }

    void Update()
    {
        IsWalking = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        IsRunning = Input.GetKey(KeyCode.LeftShift);

        HandleMovement(IsWalking, IsRunning);
        StartRoom();
    }

    void HandleMovement(bool isWalking, bool isRunning)
    {
        if (isWalking && _animationSpeed < _maxWalkSpeed)
        {
            _animationSpeed += Time.deltaTime * _acceleration;
        }

        if (isWalking && isRunning && _animationSpeed < _maxRunningSpeed)
        {
            _animationSpeed += Time.deltaTime * _acceleration;
        }

        if (!isRunning && _animationSpeed > 0.5f)
        {
            _animationSpeed -= Time.deltaTime * _deceleration;
        }

        if (!isWalking && _animationSpeed > 0f)
        {
            _animationSpeed -= Time.deltaTime * _deceleration;
        }

        _animator.SetFloat(_hashVelocityZ, _animationSpeed);
    }

    void StartRoom()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _animator.SetLayerWeight(_spellLayerIndex, 1f);
            _animator.Play("Humanoid Spell", _spellLayerIndex, 0f);
        }
    }

    void EndAnimation(string layerName)
    {
        _animator.SetLayerWeight(_spellLayerIndex, 0f);
    }
}
