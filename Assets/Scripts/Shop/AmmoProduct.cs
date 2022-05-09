using UnityEngine;

public class AmmoProduct : Product
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _ammo;

    public Weapon Weapon => _weapon;
    public int Ammo => _ammo;

    public void Initialize(Weapon weapon, int ammo, GameObject prefab)
    {
        _weapon = weapon;
        _ammo = ammo;
        _productBasePrefab = prefab;
        Name = $"{Weapon.Name} ammo";
        Description = $"Adds {ammo} ammo to {weapon.Name}";
        Price = weapon.Price / 5;
        Icon = _weapon.AmmoIcon;
        Player = FindObjectOfType<Player>();
        InitializeUI();
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public override void BuyProduct()
    {
        if (Player.CanAffordProduct(Price, this))
            Player.BuyProduct(Price, this);
    }

    public override void InitializeProduct()
    {
    }
}
