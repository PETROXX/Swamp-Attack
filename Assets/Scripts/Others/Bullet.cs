using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _speed = 20f;
    private Vector3 _direction;
    private Rigidbody2D _rig;

    public void InitializeBullet(float damage, float speed, Vector3 direction)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction;
    }

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _rig.velocity = _direction * _speed;
        StartCoroutine(DestructionCooldown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.GetDamage(_damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestructionCooldown()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
