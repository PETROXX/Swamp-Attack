using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerShooting _playerShooting;

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _cashText;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _killsText;
    [SerializeField] private Image _weaponIcon;

    private void Start()
    {
        _player.OnPlayerHealthChanged += UpdateHealthText;
        _player.OnEnemyKilled += UpdateCashAndKillsText;

        _playerShooting.OnShootPerformed += UpdateWeaponUI;
        _playerShooting.OnWeaponSwitched += UpdateWeaponIcon;
    }

    private void UpdateWeaponIcon()
    {
        _weaponIcon.sprite = _playerShooting.CurrentWeapon.WeaponIcon;
    }

    private void UpdateCashAndKillsText()
    {
        _cashText.text = $"Cash: {_player.Cash}$";
        _killsText.text = $"{_player.KillsAmount} kills";
    }

    private void UpdateHealthText()
    {
        _healthText.text = $"HP:{_player.Health}";
    }

    private void UpdateWeaponUI()
    {
        Weapon currentWeapon = _playerShooting.CurrentWeapon;

        if (!currentWeapon.NoAmmo)
        {
            if (!currentWeapon.IsReloading)
            {
                if (!currentWeapon.IsInfiniteAmmo)
                    _ammoText.text = $"Ammo: {currentWeapon.CurrentAmmo}/{currentWeapon.Ammo}";
                else
                    _ammoText.text = $"Ammo: {currentWeapon.CurrentAmmo}/{currentWeapon.CellSize}";
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

    }
}
