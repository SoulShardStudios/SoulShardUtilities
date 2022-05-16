namespace SoulShard.InventorySystem
{
    /// <summary>
    /// An example base item.
    /// </summary>
    public class BaseItem : IBaseItem
    {
        public BaseItem(bool isStackable = false, uint maxStackAmount = 0, string name = "")
        {
            this.isStackable = isStackable;
            this.maxStackAmount = maxStackAmount;
            this.name = name;
        }

        public bool isStackable { get; private set; }
        public uint maxStackAmount { get; private set; }
        public string name { get; private set; }
    }
}
