
using System;
using UnityEngine;   

public class InventorySlot : IReadOnlyInventorySlot
{
    public event Action<string> ItemIdChanged;
    public event Action<int> ItemAmountChanged;
    public event Action<Sprite> ItemImageChanged;

    private InventorySlotData _data;

    public InventorySlot(InventorySlotData data)
    {
        _data = data;
        DefaultImage = _data.DefaultIcon;
    }

    public string ItemId
    {
        get => _data.ItemId;
        set 
        { 
            if(_data.ItemId != value)
            {
                _data.ItemId = value;
                ItemIdChanged?.Invoke(value);
            }
        }
    }

    public int Amount
    {
        get => _data.Amount;
        set
        {
            if(_data.Amount != value)
            {
                _data.Amount = value;
                ItemAmountChanged?.Invoke(value);
            }
        }
    }

    public Sprite Image
    {
        get => _data.ItemIcon;
        set
        {
            if (_data.ItemIcon != value)
            {
                _data.ItemIcon = value;
                ItemImageChanged?.Invoke(value);
            }
        }
    }

    public bool IsEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);

    public Sprite DefaultImage
    {
        get => _data.DefaultIcon;
        set
        {
            if (_data.DefaultIcon != value)
            {
                _data.DefaultIcon = value;
            }
        }
    }
}
