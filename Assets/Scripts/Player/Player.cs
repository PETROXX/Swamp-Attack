using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimation))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _cash;
    [SerializeField] private int _killsAmount;

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _cashText;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _killsText;

    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _currentWeapon;

    [SerializeField] private Image _weaponIcon;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private GameObject _explosionFX;

    [SerializeField] private TextController _textController;

    [SerializeField] private GameObject _shopPanel;

    private Spawner _spawner;

    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimations;
    private bool _isDead;

    private float _nextFire;

    public bool IsDead => _isDead;

    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _isDead = false;
        _nextFire = 0f;

        foreach(Weapon weapon in _weapons)
        {
            weapon.InitializeWeapon(_bulletSpawnPosition, this);
        }

        _playerMovement = GetComponent<PlayerMovement>();
        _currentWeapon = _weapons[0];
        _playerAnimations = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        UpdateUI();
        HandleShootingInput();

        if (Input.GetKeyDown(KeyCode.Tab))
            _shopPanel.SetActive(!_shopPanel.activeInHierarchy);

    }

    //В отдельный класс
    private void HandleShootingInput()
    {
        if (_shopPanel.activeInHierarchy)
            return;

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

    public void GetDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
        else
            _playerAnimations.DamagedAnimation();
    }

    public void RestoreHealth(float health)
    {
        _health += health;
    }

    private void Die()
    {
        _spawner.StopSpawner();
        _explosionFX.SetActive(true);
        StartCoroutine(CooldownAfterDeath());
        _textController.SetText("Game Over!", Color.red);
    }

    private IEnumerator CooldownAfterDeath()
    {
        yield return new WaitForSeconds(0.7f);

        GetComponent<SpriteRenderer>().enabled = false;
        _isDead = true;
    }

    // В отдельный класс
    private void UpdateUI()
    {
        _healthText.text = $"HP:{_health}";
        _cashText.text = $"Cash: {_cash}$";

        if (!_currentWeapon.NoAmmo)
        {
            if (!_currentWeapon.IsReloading)
            {
                if (!_currentWeapon.IsInfiniteAmmo)
                    _ammoText.text = $"Ammo: {_currentWeapon.CurrentAmmo}/{_currentWeapon.Ammo}";
                else
                    _ammoText.text = $"Ammo: {_currentWeapon.CurrentAmmo}/{_currentWeapon.CellSize}";
            }
            else
            {
                _ammoText.text = "Reloading...";
            }
        }
        else
        {
            _ammoText.text = "No ammo!";
        }

        _weaponIcon.sprite = _currentWeapon.WeaponIcon;
        _killsText.text = $"{_killsAmount} kills";
    }

    //В событие
    public void KilledEnemy(int cash)
    {
        _cash += cash;
        _killsAmount++;
    }

    public bool TryBuyProduct(float productPrice, Product product)
    {
        if (_cash >= productPrice)
        {
            _cash -= productPrice;
            if (product is WeaponProduct)
            {
                Weapon weapon = ((WeaponProduct)product).Weapon;
                weapon.InitializeWeapon(_bulletSpawnPosition, this);
                _weapons.Add(weapon);
            }
            if (product is AmmoProduct)
            {
                AmmoProduct ammoProduct = (AmmoProduct)product;
                ammoProduct.Weapon.AddAmmo(ammoProduct.Ammo);
            }
            if (product is HealthProduct)
            {
                HealthProduct healthProduct = (HealthProduct)product;
                RestoreHealth(healthProduct.Heal);
            }    
            return true;
        }
        else
            return false;
    }
}
