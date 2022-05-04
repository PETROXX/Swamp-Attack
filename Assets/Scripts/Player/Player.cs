using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerShooting))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _cash;
    [SerializeField] private int _killsAmount;

    // Вынести в отдельный класс
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _cashText;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _killsText;

    [SerializeField] private Weapon _currentWeapon;

    [SerializeField] private Image _weaponIcon;
    [SerializeField] private GameObject _explosionFX;

    [SerializeField] private TextController _textController;

    [SerializeField] private GameObject _shopPanel;

    private Spawner _spawner;

    private PlayerAnimation _playerAnimations;
    private PlayerShooting _playerShooting;
    private bool _isDead;

    public bool IsDead => _isDead;
    public float Health => _health;
    public float Cash => _cash;
    public int KillsAmount => _killsAmount;

    public event Action OnPlayerHealthChanged;
    public event Action OnEnemyKilled;

    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _isDead = false;

        _playerAnimations = GetComponent<PlayerAnimation>();
        _playerShooting = GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            _shopPanel.SetActive(!_shopPanel.activeInHierarchy);
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
        OnPlayerHealthChanged?.Invoke();

        if (_health <= 0)
            Die();
        else
            _playerAnimations.DamagedAnimation();
    }

    public void RestoreHealth(float health)
    {
        _health += health;
        OnPlayerHealthChanged?.Invoke();
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


    //В событие
    public void KilledEnemy(int cash)
    {
        _cash += cash;
        _killsAmount++;
        OnEnemyKilled?.Invoke();
    }

    public void BuyProduct(float price, Product product)
    {
        _cash -= price;

        if (product is WeaponProduct)
        {
            Weapon weapon = ((WeaponProduct)product).Weapon;
            _playerShooting.AddWeapon(weapon);
        }

        if (product is AmmoProduct)
        {
            AmmoProduct ammoProduct = (AmmoProduct)product;
            _playerShooting.AddAmmo(ammoProduct.Weapon, ammoProduct.Ammo);
        }

        if (product is HealthProduct)
        {
            HealthProduct healthProduct = (HealthProduct)product;
            RestoreHealth(healthProduct.Heal);
        }
    }

    public bool CanAffordProduct(float productPrice, Product product)
    {
        return _cash >= productPrice;
    }
}
