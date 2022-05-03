using UnityEngine;

public class WeaponProduct : Product
{
    [SerializeField] private Weapon _weapon;

    public Weapon Weapon => _weapon;

    private Shop _shop;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        _shop = FindObjectOfType<Shop>();
        InitializeProduct();
        InitializeUI();
    }

    public override void BuyProduct()
    {
        if (Player.CanAffordProduct(Price, this))
        {
            Player.BuyProduct(Price, this);
            Destroy(gameObject);
            _shop.AddAmmoProduct(Weapon.AmmoPrefab, this);
        }
    }

    public override void InitializeProduct()
    {
        Icon = _weapon.WeaponIcon;
        Name = _weapon.Name;
        Description = _weapon.Desription;
        Price = _weapon.Price;
    }
}
