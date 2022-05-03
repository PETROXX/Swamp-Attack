using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Create New Weapon")]

public class Weapon : ScriptableObject
{
    [Header("Парамеры оружия для магазина")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private Sprite _ammoSprite;
    [SerializeField] private GameObject _ammoProductPrefab;

    [Header("Стрелковые параметры оружия")]
    [SerializeField] private float _damage;
    [Tooltip("Время между выстрелами. При 0 оружие не будет автоматическим")]
    [SerializeField] private float _fireRate;
    [SerializeField] private int _cellSize;
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private bool _isInfiniteAmmo;
    [SerializeField] private int _ammo;

    [Header("Параметры пули")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;

    private Transform _bulletSpawnPosition;
    private MonoBehaviour _monoBehaviour;

    private int _currentAmmo;
    private bool _isReloading;

    public string Name => _name;
    public string Desription => _description;
    public int Price  => _price;
    public Sprite WeaponIcon => _weaponIcon;
    public Sprite AmmoIcon => _ammoSprite;
    public GameObject AmmoPrefab => _ammoProductPrefab;
    public bool IsInfiniteAmmo => _isInfiniteAmmo;
    public int Ammo => _ammo;
    public int CurrentAmmo => _currentAmmo;
    public int CellSize => _cellSize;
    public float FireRate => _fireRate;
    public bool IsReloading => _isReloading;
    public bool NoAmmo { get => Ammo + CurrentAmmo == 0; }

    public void InitializeWeapon(Transform bulletSpawnPosition, MonoBehaviour monoBehaviour)
    {
        _bulletSpawnPosition = bulletSpawnPosition;
        _monoBehaviour = monoBehaviour;
        _currentAmmo = CellSize;
        _isReloading = false;
        _ammo = _cellSize * 3;
    }

    public void Shoot(Vector3 shootDir)
    {
        if(_isReloading)
            return;
        if (_currentAmmo > 0)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().InitializeBullet(_damage, _bulletSpeed, shootDir);
            _currentAmmo--;
        }
        else if(!_isReloading)
        {
            Reload();
        }
    }

    public void Reload()
    {
        _isReloading = true;
        _monoBehaviour.StartCoroutine(ReloadCooldown(_reloadSpeed));
    }

    private IEnumerator ReloadCooldown(float cooldown)
    {
        if(_isInfiniteAmmo)
        {
            yield return new WaitForSeconds(cooldown);
            _currentAmmo = _cellSize;
            _isReloading = false;
        }
        else if (_ammo != 0)
        {
            yield return new WaitForSeconds(cooldown);
            _ammo += _currentAmmo;
            if (_ammo >= _cellSize)
            {
                _ammo -= _cellSize;
                _currentAmmo = _cellSize;
            }
            else
            {
                _currentAmmo = _ammo;
                _ammo = 0;
            }
            _currentAmmo = _cellSize;
            _isReloading = false;
        }
    }

    public void AddAmmo(int ammo)
    {
        _ammo += ammo;
    }
}