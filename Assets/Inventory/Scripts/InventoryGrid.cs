using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGrid : IReadOnlyInventoryGrid
{
    private readonly InventoryGridData _data;
    private readonly Dictionary<Vector2Int, InventorySlot> _slotMap = new();

    public InventoryGrid(InventoryGridData data)
    {
        _data = data;
        var size = _data.Size;
        for(var i = 0; i < size.x; i ++)
        {
            for(var j = 0; j < size.y; j++)
            {
                var index = i * size.y + j;
                var slotData = data.Slots[index];
                var slot = new InventorySlot(slotData);
                var position = new Vector2Int(i, j);
                _slotMap[position] = slot;
            }
        }
    }


    public Vector2Int Size//Нужно удалить set
    {
        get => _data.Size;
        set
        {
            if (_data.Size != value)
            {
                _data.Size = value;
            }
        }
    }

    public int GetAmount(string itemId)
    {
        var amount = 0;
        var slots = _data.Slots;

        foreach(var slot in slots) 
        {
            if(slot.ItemId == itemId)
            {
                amount += slot.Amount;
            }
        }
        return amount;
    }

    public IReadOnlyInventorySlot[,] GetSlots()
    {
        var array = new IReadOnlyInventorySlot[Size.x, Size.y];

        for(var i = 0; i < Size.x; i++)
        {
            for (var j = 0;j < Size.y; j++)
            {
                var position = new Vector2Int(i, j);
                array[i,j] = _slotMap[position];
            }
        }
        return array;
    }

    public bool Has(string itemId, int amount)
    {
        var amountExist = GetAmount(itemId);
        return amountExist >= amount;
    }

    public int GetItemSlotCapacity(string itemId)
    {
        return 100;
    }

    public AddItemsToInventoryResult AddItems(string itemId, int amount, Sprite icon)
    {
        var remainingAmount = amount;
        var itemsAddedToSlotsWithSameItemsAmount = AddToSlotsWithSameItems(itemId, remainingAmount, out remainingAmount);

        if(remainingAmount == 0)
        {
            return new AddItemsToInventoryResult(amount, itemsAddedToSlotsWithSameItemsAmount);
        }

        var itemsAddedToAvailableSlotsAmount = AddToFirstAvailableSlots(itemId, remainingAmount, icon, out remainingAmount);
        var totalAddedItemsAmount = itemsAddedToSlotsWithSameItemsAmount + itemsAddedToAvailableSlotsAmount;

        return new AddItemsToInventoryResult(amount, totalAddedItemsAmount);
    }

    public RemoveItemsFromInventoryResult RemoveItems(string itemId, int amount)
    {
        if(!Has(itemId, amount))
        {
            return new RemoveItemsFromInventoryResult(amount, false);
        }

        var amountToRemove = amount;
        for (var i = 0; i < Size.x; i++)
        {
            for (var j = 0; j < Size.y; j++)
            {
                var coords = new Vector2Int(i, j);
                var slot = _slotMap[coords];

                if(slot.ItemId != itemId)
                {
                    continue;
                }

                if(amountToRemove > slot.Amount)
                {
                    amountToRemove -= slot.Amount;
                    slot.Amount = 0;
                }
                else
                {
                    slot.Amount -= amountToRemove;
                    amountToRemove = 0;
                }

                if(slot.Amount == 0)
                {
                    slot.ItemId = null;
                    slot.Image = null;//Нужно подумать над станадртным отображением иконки
                }

                if (amountToRemove == 0)
                    return new RemoveItemsFromInventoryResult(amount, true);
            }    
        }
        throw new Exception("Что-то не так с удалением предмета из инвентаря");
    }

    private int AddToSlotsWithSameItems(string itemId, int amount, out int remainingAmount)
    {
        var itemsAddedAmount = 0;
        remainingAmount = amount;

        for (var i = 0;i < Size.x;i++)
        {
            for(var j = 0; j < Size.y; j++)
            {
                var coords = new Vector2Int(i, j);
                var slot = _slotMap[coords];

                if(slot.IsEmpty)
                {
                    continue;
                }

                var slotItemCapacity = GetItemSlotCapacity(itemId);

                if (slot.Amount == slotItemCapacity || slot.ItemId != itemId)
                {
                    continue;
                }

                var newValue = slot.Amount + remainingAmount;
                if (newValue > slotItemCapacity)
                {
                    remainingAmount = newValue - slotItemCapacity;
                    var itemsToAddedAmount = slotItemCapacity - slot.Amount;
                    itemsAddedAmount += itemsToAddedAmount;
                    slot.Amount = slotItemCapacity;
                }
                else
                {
                    itemsAddedAmount += remainingAmount;
                    slot.Amount = newValue;
                    remainingAmount = 0;

                    return itemsAddedAmount;
                }
            }
        }
        return itemsAddedAmount;
    }
    private int AddToFirstAvailableSlots(string itemId, int amount, Sprite icon, out int remainingAmount)
    {
        var itemsAddedAmount = 0;
        remainingAmount = amount;

        for(var i = 0; i < Size.x; i++)
        {
            for (var j = 0; j < Size.y; j++)
            {
                var coords = new Vector2Int(i, j);
                var slot = _slotMap[coords];

                if(!slot.IsEmpty)
                {
                    continue;
                }

                slot.ItemId = itemId;
                slot.Image = icon;
                var newValue = slot.Amount + remainingAmount;
                var slotItemCapacity = GetItemSlotCapacity(itemId);

                if(newValue > slotItemCapacity)
                {
                    remainingAmount -= slotItemCapacity;
                    var itemsToAddAmount = slotItemCapacity - slot.Amount;
                    itemsAddedAmount += itemsToAddAmount;
                    slot.Amount = slotItemCapacity;
                }
                else
                {
                    itemsAddedAmount += remainingAmount;
                    slot.Amount += newValue;
                    remainingAmount = 0;
                    return itemsAddedAmount;
                }
            }
        }
        return itemsAddedAmount;
    }


}
