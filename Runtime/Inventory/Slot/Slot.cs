namespace SoulShard.Inventory
{
    /// <summary>
    /// The basic item slot manager.
    /// </summary>
    /// <typeparam name="BaseItem">The base item type this slot stores.</typeparam>
    public class Slot<BaseItem> : ISlot<BaseItem> where BaseItem : class, IBaseItem
    {
        ItemInstance<BaseItem> item;

        public ItemInstance<BaseItem> itemInstance
        {
            get => item;
            set
            {
                item = value;
                onItemModified?.Invoke(itemInstance);
            }
        }

        public System.Action<ItemInstance<BaseItem>> onItemModified { get; set; }

        public Slot(ItemInstance<BaseItem> item) => itemInstance = item;

        public Slot() => itemInstance = new ItemInstance<BaseItem>();

        public virtual ItemInstance<BaseItem> Transfer(string button, ItemInstance<BaseItem> other)
        {
            ItemInstance<BaseItem> originalItem = other;
            other = itemInstance;
            itemInstance = originalItem;
            onItemModified?.Invoke(itemInstance);
            return originalItem;
        }
    }
}
