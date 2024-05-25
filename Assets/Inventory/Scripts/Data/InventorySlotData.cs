using System;
using UnityEngine;


[Serializable]
public class InventorySlotData
{
    public string ItemId;
    public int Amount;
    public Sprite ItemIcon;
    public Sprite DefaultIcon;

    public InventorySlotData(Sprite _sprite)
    {
        DefaultIcon = _sprite;
        ItemIcon = _sprite;
    }
}
