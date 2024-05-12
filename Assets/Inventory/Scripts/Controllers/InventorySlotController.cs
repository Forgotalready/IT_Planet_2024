public class InventorySlotController
{
    private readonly InventorySlotView _slotView;

    public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view)
    {
        _slotView = view;

        slot.ItemIdChanged += OnSlotItemIdChanged;
        slot.ItemAmountChanged += OnSlotItemAmountChanged;
        slot.ItemImageChanged += OnSlotItemImageChanged;

        view.ItemName = slot.ItemId;
        view.ItemAmount = slot.Amount;
        view.SetIcon(slot.Image);
    }

    private void OnSlotItemImageChanged(UnityEngine.Sprite newIcon)
    {
        _slotView.SetIcon(newIcon);
    }

    private void OnSlotItemAmountChanged(int newItemAmount)
    {
        _slotView.ItemAmount = newItemAmount;
    }

    private void OnSlotItemIdChanged(string newItemName)
    {
        _slotView.ItemName = newItemName;
    }
}
