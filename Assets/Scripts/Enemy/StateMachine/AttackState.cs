using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]

public class AttackState : State
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackDistance;

    private Animator _animator;
    private Enemy _enemy;

    private float _damage;
    private bool _isInCooldown;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();

        _enemy.OnDamageUpdated += ChangeDamage;

        _damage = _enemy.Damage;
    }

    private void ChangeDamage()
    {
        _damage = _enemy.Damage;
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

    private IEnumerator WaitForAttackCooldown(float cooldown)
    {
        _isInCooldown = true;
        yield return new WaitForSeconds(cooldown);
        _isInCooldown = false;
    }
}
