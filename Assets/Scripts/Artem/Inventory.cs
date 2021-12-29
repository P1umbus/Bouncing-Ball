using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text _textButton;
    [SerializeField] private ItemManager[] _itemManagers;

    [SerializeField] private Dropdown _dropdown;
    [SerializeField] private DropdownHanler _dropdownHanler;

    private bool _isShop = true;

    private enum Texts
    {
        Inventory,
        Shop
    }

    public void OnClick()
    {
        _isShop = !_isShop;

        if (_isShop == true)
            _textButton.text = Texts.Inventory.ToString();
        else
            _textButton.text = Texts.Shop.ToString();

        if (_isShop == false)
        {
            foreach (ItemManager item in _itemManagers)
            {
                if (item.IsItemBougnt())
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            }
        }
        else
        {
            _dropdownHanler.Sort(_dropdown.value);
        }
    }
}
