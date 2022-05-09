using UnityEngine;
using System.Collections;
using System;

using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyRemoteConfig _remoteConfig;

    [SerializeField] private float _health;
    [SerializeField] private int _rewardForKill;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private Transform _target;
    private Player _player;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public bool IsDead { get; private set; }
    public float Speed => _speed;
    public float Damage => _damage;

    public event Action OnSpeedUpdated;
    public event Action OnDamageUpdated;

    private void Start()
    {
        _remoteConfig.OnParametersLoaded += ChangeParameters;
        _player = FindObjectOfType<Player>();
        _target = _player.transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health *= Random.Range(0.5f, 2f);
    }

    private void ChangeParameters(float health, float speed, float damage)
    {
        _health = health;
        _speed = speed;
        _damage = damage;

        OnSpeedUpdated?.Invoke();
        OnDamageUpdated?.Invoke();
    }

    public void GetDamage(float damage)
    { 
        _health -= damage;

        if (_health <= 0)
            Die();
        else
            StartCoroutine(PlayerDamagedGrahics());

        _rigidbody.AddForce((transform.position - _target.position) * 20f);
    }

    private IEnumerator PlayerDamagedGrahics()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        _player.KilledEnemy(_rewardForKill);
        _rigidbody.bodyType = RigidbodyType2D.Static;
        IsDead = true;
        _collider.enabled = false;
        _animator.SetTrigger("IsDead");
    }
}
