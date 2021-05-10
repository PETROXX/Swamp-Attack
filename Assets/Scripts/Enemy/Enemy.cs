using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _rewardForKill;
    private Transform _target;
    private Player _player;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rig;

    public bool IsDead { get; private set; }

    void Start()
    {
        _target = GameObject.Find("Player").GetComponent<Transform>();
        _player = _target.GetComponent<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rig = GetComponent<Rigidbody2D>();
        _health *= Random.Range(0.5f, 2f);
    }

    public void GetDamage(float damage)
    { 
        _health -= damage;
        if (_health <= 0)
            Die();
        else
            StartCoroutine(GetDamageGFX());
        _rig.AddForce((transform.position - _target.position) * 20f);
    }

    IEnumerator GetDamageGFX()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        _player.KilledEnemy(_rewardForKill);
        _rig.bodyType = RigidbodyType2D.Static;
        IsDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("IsDead");
    }
}
