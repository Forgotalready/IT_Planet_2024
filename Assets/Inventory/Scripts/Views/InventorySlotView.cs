using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemAmount;
    [SerializeField] private Image _itemIcon;


    public string ItemName
    {
        get => _itemName.text;
        set => _itemName.text = value;
    }

    public int ItemAmount
    {
        get => Convert.ToInt32(_itemAmount.text);
        set => _itemAmount.text = value <= 1 ? "" : value.ToString();
    }

    public Sprite GetIcon()
    {
        return _itemIcon.sprite;
    }

    public void SetIcon(Sprite icon)
    {
        _itemIcon.sprite = icon;
    }
}

