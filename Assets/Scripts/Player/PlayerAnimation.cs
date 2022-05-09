using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _movement;
    private Player _player;
    private Animator _animator;

    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player.IsDead)
            return;
        _animator.SetBool("IsMoving", _movement.IsMoving);
        _animator.SetBool("IsJumping", _movement.IsJumping);
    }

    public void DamagedAnimation()
    {
        _animator.SetTrigger("Damaged");
    }
}
