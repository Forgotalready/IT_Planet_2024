using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InventoryGridView _inventoryGridView;
    private InventoryGrid _inventoryGrid;
    private InventoryGridController _inventoryGridController;
    [SerializeField] Sprite _sprite;


    public static Action<string> BEDA;

    public static EntryPoint Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var inventoryData = CreateTestInventory();
        _inventoryGrid = new InventoryGrid(inventoryData);
        _inventoryGridController = new InventoryGridController(_inventoryGrid, _inventoryGridView);
        InventorySlotView.onClick += SelectedItem;
    }

    public AddItemsToInventoryResult AddItem(string itemId, int amount, Sprite icon)
    {
        return _inventoryGrid.AddItems(itemId, amount, icon); 

    }

    public RemoveItemsFromInventoryResult RemoveItems(string itemId, int amount)
    {
        return _inventoryGrid.RemoveItems(itemId, amount);
    }

    private InventoryGridData CreateTestInventory()
    {
        var size = new Vector2Int(1, 8);
        var createdInventorySlots = new List<InventorySlotData>();
        var length = size.x * size.y;

        for (var i = 0; i < length; i++)
        {
            createdInventorySlots.Add(new InventorySlotData(_sprite));
        }

        var createdInventoryData = new InventoryGridData
        {
            Size = size,
            Slots = createdInventorySlots
        };

        return createdInventoryData;
    }

    public void SelectedItem(string itemName)
    {
        BEDA?.Invoke(itemName);
    }
}
