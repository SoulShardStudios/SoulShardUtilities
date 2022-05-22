namespace SoulShard.InventorySystem
{
    /// <summary>
    /// The basic item slot manager.
    /// </summary>
    /// <typeparam name="_BaseItem">The base item type this slot stores.</typeparam>
    public class Slot<_BaseItem, _ItemInstance> : ISlot<_BaseItem, _ItemInstance> 
        where _BaseItem : class, IBaseItem 
        where _ItemInstance: struct, IItemInstance<_BaseItem>
    {
        _ItemInstance item;
        public System.Action<_ItemInstance> onItemModified { get; set; }
        public _ItemInstance itemInstance
        {
            get => item;
            set
            {
                item = value;
                onItemModified?.Invoke(itemInstance);
            }
        }

        public Slot(_ItemInstance item) => itemInstance = item;

        public Slot() => itemInstance = new _ItemInstance();

        public bool isEmpty
        {
            get => item.item == null;
        }

        public virtual _ItemInstance Transfer(
            _ItemInstance other,
            string button = ""
        )
        {
            _ItemInstance originalItem = itemInstance;
            itemInstance = other;
            onItemModified?.Invoke(itemInstance);
            return originalItem;
        }
    }
}
