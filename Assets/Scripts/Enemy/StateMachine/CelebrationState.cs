using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationState : State
{
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("PlayerDead", true);
    }

    private void OnDisable()
    {
        _animator.SetBool("PlayerDead", false);
    }
}
