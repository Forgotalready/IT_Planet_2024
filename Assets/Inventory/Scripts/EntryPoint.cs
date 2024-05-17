using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InventoryGridView _inventoryGridView;
    private InventoryGrid _inventoryGrid;
    private InventoryGridController _inventoryGridController;

    private void Start()
    {
        var inventoryData = CreateTestInventory();
        _inventoryGrid = new InventoryGrid(inventoryData);
        _inventoryGridController = new InventoryGridController(_inventoryGrid, _inventoryGridView);
    }

    public AddItemsToInventoryResult AddItem(string itemId, int amount, Sprite icon)
    {
        return _inventoryGrid.AddItems(itemId, amount, icon);
    }

    private InventoryGridData CreateTestInventory()
    {
        var size = new Vector2Int(1, 8);
        var createdInventorySlots = new List<InventorySlotData>();
        var length = size.x * size.y;

        for (var i = 0; i < length; i++)
        {
            createdInventorySlots.Add(new InventorySlotData());
        }

        var createdInventoryData = new InventoryGridData
        {
            Size = size,
            Slots = createdInventorySlots
        };

        return createdInventoryData;
    }
}
