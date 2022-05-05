using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class MoveState : State
{
    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rig;

    private void Start()
    {
        _speed = Random.Range(_speed, _speed * 2f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rig = GetComponent<Rigidbody2D>();
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

        _rig.MovePosition(new Vector2(_rig.position.x + _speed * Time.deltaTime * direction, _rig.position.y));
    }
}
