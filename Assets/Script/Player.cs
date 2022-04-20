using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private int _spellLayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spellLayerIndex = _animator.GetLayerIndex("Spell Layer");
    }

    // Update is called once per frame
    void Update()
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
