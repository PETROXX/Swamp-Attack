using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;

    private Player _player;
    private Rigidbody2D _playerRig;
    private SpriteRenderer _playerSprite;

    public bool IsMoving { get; private set; }
    public bool IsJumping => !_isGrounded;

    public Vector3 LookDirection
    {
        get
        {
            if (_playerSprite.flipX)
                return -transform.right;
            else
                return transform.right;
        }
    }

    private void Start()
    {
        _playerRig = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player.IsDead)
            return;

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.15f, _groundLayer);
        float moveDir = Input.GetAxis("Horizontal");
        IsMoving = Mathf.Abs(moveDir) >= 0.1;

        if (moveDir <= 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = Vector3.zero;

        transform.position += new Vector3(moveDir * _speed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
    }

    private void Jump()
    {
        _playerRig.AddForce(Vector2.up * _jumpForce);
    }
}
