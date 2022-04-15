using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private float _velocity = 0f;
    [SerializeField]
    private float _acceleration = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var forwardPressed = Input.GetKey(KeyCode.W);

        if (forwardPressed && _velocity < 5f)
        {
            _velocity += Time.deltaTime * _acceleration;
        }

        if (!forwardPressed && _velocity > 0f)
        {
            _velocity -= Time.deltaTime * _acceleration;
        }

        if (!forwardPressed && _velocity < 0f)
        {
            _velocity = 0f;
        }

        animator.SetFloat("Velocity", _velocity);

    }
}
