
public struct AddItemsToInventoryResult
{
    public readonly int ItemsToAddAmount;
    public readonly int ItemsAddedAmount;

    public int ItemsNotAddedAmount => ItemsToAddAmount - ItemsAddedAmount;

    public AddItemsToInventoryResult(int itemsToAddAmount, int itemsAddedAmount)
    {
        ItemsToAddAmount = itemsToAddAmount;
        ItemsAddedAmount = itemsAddedAmount;
    }
}
