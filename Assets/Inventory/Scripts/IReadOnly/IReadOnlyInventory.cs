public interface IReadOnlyInventory
{
    int GetAmount(string itemId);
    bool Has(string itemId, int amount);
}
