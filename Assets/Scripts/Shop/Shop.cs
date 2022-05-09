using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;

    public void AddAmmoProduct(GameObject prefab, WeaponProduct weaponProduct)
    {
        GameObject g = Instantiate(prefab, _parentObject);
        Weapon weapon = weaponProduct.Weapon;
        g.GetComponent<AmmoProduct>().Initialize(weapon, weapon.CellSize, weapon.AmmoPrefab);
    }
}