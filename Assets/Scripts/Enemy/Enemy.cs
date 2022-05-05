using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _health;
    [SerializeField] private int _rewardForKill;

    private Transform _target;
    private Player _player;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public bool IsDead { get; private set; }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _target = _player.transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health *= Random.Range(0.5f, 2f);
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
