using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackDistance;

    private bool _isInCooldown;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) < _attackDistance)
            Attack();
    }

    private void Attack()
    {
        if (_isInCooldown)
            return;
        _animator.SetTrigger("Attack");
        Target.GetDamage(_damage);
        StartCoroutine(WaitForAttackCooldown(_attackCooldown));
    }

    IEnumerator WaitForAttackCooldown(float cooldown)
    {
        _isInCooldown = true;
        yield return new WaitForSeconds(cooldown);
        _isInCooldown = false;
    }
}
