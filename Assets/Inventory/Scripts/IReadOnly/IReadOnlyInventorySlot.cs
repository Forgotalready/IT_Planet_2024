using System;
using UnityEngine;

public interface IReadOnlyInventorySlot
{
    event Action<string> ItemIdChanged;
    event Action<int> ItemAmountChanged;
    event Action<Sprite> ItemImageChanged;

    string ItemId {  get; }
    int Amount { get; }
    Sprite Image { get; }
    bool IsEmpty { get; }
}
