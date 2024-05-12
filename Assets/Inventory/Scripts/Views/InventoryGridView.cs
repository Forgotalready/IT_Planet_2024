using UnityEngine;

public class InventoryGridView : MonoBehaviour
{
    [SerializeField] private InventorySlotView[] _slots;

    public InventorySlotView GetInventorySlotView(int index)
    {
        return _slots[index];
    }
}
