namespace SoulShard.InventorySystem
{
    /// <summary>
    /// The basic item slot manager.
    /// </summary>
    /// <typeparam name="_BaseItem">The base item type this slot stores.</typeparam>
    public class Slot<_BaseItem> : ISlot<_BaseItem> where _BaseItem : class, IBaseItem
    {
        ItemInstance<_BaseItem> item;

        public ItemInstance<_BaseItem> itemInstance
        {
            get => item;
            set
            {
                item = value;
                onItemModified?.Invoke(itemInstance);
            }
        }

        public System.Action<ItemInstance<_BaseItem>> onItemModified { get; set; }

        public Slot(ItemInstance<_BaseItem> item) => itemInstance = item;

        public Slot() => itemInstance = new ItemInstance<_BaseItem>();

        public bool isEmpty
        {
            get => item.item == null;
        }

        public virtual ItemInstance<_BaseItem> Transfer(
            ItemInstance<_BaseItem> other,
            string button = ""
        )
        {
            ItemInstance<_BaseItem> originalItem = itemInstance;
            itemInstance = other;
            onItemModified?.Invoke(itemInstance);
            return originalItem;
        }
    }
}
