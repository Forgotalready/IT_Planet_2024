using UnityEngine;

public interface IReadOnlyInventoryGrid: IReadOnlyInventory
{
    Vector2Int Size { get; }
    IReadOnlyInventorySlot[,] GetSlots();
}
