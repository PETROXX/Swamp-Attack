using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]

public class MoveState : State
{
    private float _speed;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _enemy.OnSpeedUpdated += ChangeSpeed;

        _speed = _enemy.Speed;
        _speed = Random.Range(_speed, _speed * 1.5f);
    }

    private void ChangeSpeed()
    {
        _speed = _enemy.Speed;
    }

    private void Update()
    {
        int direction;

        if (transform.position.x - Target.transform.position.x < 0)
        {
            _spriteRenderer.flipX = false;
            direction = 1;
        }
        else
        {
            _spriteRenderer.flipX = true;
            direction = -1;
        }

        _rigidbody.MovePosition(new Vector2(_rigidbody.position.x + _speed * Time.deltaTime * direction, _rigidbody.position.y));
    }
    
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
