using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlendController : MonoBehaviour
{
    private Animator _animator;
    private float _velocityX = 0f;
    private float _velocityZ = 0f;
    private float _maxWalkVelocity = 0.5f;
    private float _maxRunVelocity = 2f;

    [SerializeField]
    private float _acceleration = 2f;
    [SerializeField]
    private float _deceleration = 2f;

    private int _velocityZHash;
    private int _velocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _velocityXHash = Animator.StringToHash("Velocity X");
    }

    void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runningPressed, float currentMaxVelocity)
    {
        if (forwardPressed && _velocityZ < currentMaxVelocity)
        {
            _velocityZ += Time.deltaTime * _acceleration;
        }

        if (leftPressed && _velocityX > -currentMaxVelocity)
        {
            _velocityX -= Time.deltaTime * _acceleration;
        }

        if (rightPressed && _velocityX < currentMaxVelocity)
        {
            _velocityX += Time.deltaTime * _acceleration;
        }

        if (!forwardPressed && _velocityZ > 0f)
        {
            _velocityZ -= Time.deltaTime * _deceleration;
        }

        if (!leftPressed && _velocityX < 0f)
        {
            _velocityX += Time.deltaTime * _deceleration;
        }

        if (!rightPressed && _velocityX > 0f)
        {
            _velocityX -= Time.deltaTime * _deceleration;
        }
    }

    void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runningPressed, float currentMaxVelocity)
    {
        if (!forwardPressed && _velocityZ < 0f)
        {
            _velocityZ = 0f;
        }

        if (!leftPressed && !rightPressed && _velocityX != 0f && (_velocityX > -0.05f && _velocityX < 0.05f))
        {
            _velocityX = 0f;
        }

        if (forwardPressed && runningPressed && _velocityZ > currentMaxVelocity)
        {
            _velocityZ -= Time.deltaTime * _deceleration;
            if (_velocityZ > currentMaxVelocity && _velocityZ < (currentMaxVelocity + 0.05f))
            {
                _velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && _velocityZ > currentMaxVelocity)
        {
            _velocityZ -= Time.deltaTime * _deceleration;
        }
        else if (forwardPressed && _velocityZ < currentMaxVelocity && _velocityZ > (currentMaxVelocity - 0.05f))
        {
            _velocityZ = currentMaxVelocity;
        }

        if (leftPressed && runningPressed && _velocityX < -currentMaxVelocity)
        {
            _velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && _velocityX < -currentMaxVelocity)
        {
            _velocityX += Time.deltaTime * _deceleration;
            if (_velocityX < -currentMaxVelocity && _velocityX > (-currentMaxVelocity - 0.05f))
            {
                _velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && _velocityX > -currentMaxVelocity && _velocityX < (-currentMaxVelocity + 0.05f))
        {
            _velocityX = -currentMaxVelocity;
        }

        if (rightPressed && runningPressed && _velocityX > currentMaxVelocity)
        {
            _velocityX = currentMaxVelocity;
        }
        else if (rightPressed && _velocityX > currentMaxVelocity)
        {
            _velocityX -= Time.deltaTime * _deceleration;
            if (_velocityX > currentMaxVelocity && _velocityX < (currentMaxVelocity + 0.05f))
            {
                _velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && _velocityX < currentMaxVelocity && _velocityX > (currentMaxVelocity - 0.05f))
        {
            _velocityX = currentMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var forwardPressed = Input.GetKey(KeyCode.W);
        var leftPressed = Input.GetKey(KeyCode.A);
        var rightPressed = Input.GetKey(KeyCode.D);
        var runningPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runningPressed ? _maxRunVelocity : _maxWalkVelocity;

        ChangeVelocity(forwardPressed, leftPressed, rightPressed, runningPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runningPressed, currentMaxVelocity);

        _animator.SetFloat(_velocityZHash, _velocityZ);
        _animator.SetFloat(_velocityXHash, _velocityX);
    }
}
