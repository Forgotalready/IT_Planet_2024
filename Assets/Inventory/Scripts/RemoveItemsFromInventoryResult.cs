
public struct RemoveItemsFromInventoryResult
{
    public readonly int ItemsToRemoveAmount;
    public readonly bool Success;

    public RemoveItemsFromInventoryResult(int itemsToRemoveAmount, bool success)
    {
        ItemsToRemoveAmount = itemsToRemoveAmount; 
        Success = success;
    }
}