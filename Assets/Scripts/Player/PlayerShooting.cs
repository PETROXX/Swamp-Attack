using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Transform _bulletSpawnPosition;

    [SerializeField] private PlayerMovement _playerMovement;

    private float _nextFire;

    private void Start()
    {
        _nextFire = 0;

        foreach (Weapon weapon in _weapons)
        {
            weapon.InitializeWeapon(_bulletSpawnPosition, this);
        }

        _currentWeapon = _weapons[0];
    }

    private void Update()
    {
        Shooting();
        WeaponSwitchInput();
    }

    private void Shooting()
    {
        //if (_shopPanel.activeInHierarchy)
        //    return;

        if (_currentWeapon.FireRate == 0 && Input.GetButtonDown("Fire1"))
        {
            _currentWeapon.Shoot(_playerMovement.LookDirection);
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > _nextFire && _currentWeapon.FireRate > 0)
            {
                _nextFire = Time.time + _currentWeapon.FireRate;
                _currentWeapon.Shoot(_playerMovement.LookDirection);
            }
        }
    }

    private void WeaponSwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            _currentWeapon.Reload();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _currentWeapon = _weapons[0];

        if (Input.GetKeyDown(KeyCode.Alpha2) && _weapons.Count > 1)
            _currentWeapon = _weapons[1];

        if (Input.GetKeyDown(KeyCode.Alpha3) && _weapons.Count > 2)
            _currentWeapon = _weapons[2];

        if (Input.GetKeyDown(KeyCode.Alpha4) && _weapons.Count > 3)
            _currentWeapon = _weapons[3];
    }

    public void AddWeapon(Weapon weapon)
    {
        weapon.InitializeWeapon(_bulletSpawnPosition, this);
        _weapons.Add(weapon);
    }

    public void AddAmmo(Weapon weapon, int ammo)
    {
        weapon.AddAmmo(ammo);
    }
}
