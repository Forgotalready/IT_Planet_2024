using UnityEngine;

public class InventoryGridView : MonoBehaviour
{
    [SerializeField] private InventorySlotView[] _slots;

    public int GetAmountOfSlots()
    {
        return  _slots.Length;
    }

    public InventorySlotView GetInventorySlotView(int index)
    {
        return _slots[index];
    }
}
