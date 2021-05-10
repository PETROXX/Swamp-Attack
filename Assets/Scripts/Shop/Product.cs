using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Product : MonoBehaviour
{
    [SerializeField] protected Image _productSprite;
    [SerializeField] protected TMP_Text _nameText;
    [SerializeField] protected TMP_Text _desriptionText;
    [SerializeField] protected TMP_Text _priceText;
    [SerializeField] protected GameObject _productBasePrefab;

    protected Sprite Icon;
    protected float Price;
    protected string Name;
    protected string Description;

    protected Player Player;

    public GameObject ProductPrefab => _productBasePrefab;

    public abstract void InitializeProduct();

    public void InitializeUI()
    {
        _productSprite.sprite = Icon;
        _nameText.text = Name;
        _desriptionText.text = Description;
        _priceText.text = $"{Price}$";
    }

    public abstract void BuyProduct();
}
