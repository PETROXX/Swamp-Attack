using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthProduct : Product
{
    [SerializeField] private float _healAmount;
    [SerializeField] private Sprite _icon;

    public float Heal => _healAmount;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        InitializeProduct();
        InitializeUI();
    }

    public override void BuyProduct()
    {
        if (Player.CanAffordProduct(Price, this))
            Player.BuyProduct(Price, this);
    }

    public override void InitializeProduct()
    {
        Icon = _icon;
        Price = 500;
        Name = "Aid Kit";
        Description = $"Adds {_healAmount} hp to your health";
    }
}
