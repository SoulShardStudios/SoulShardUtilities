namespace SoulShard.InventorySystem
{
    /// <summary>
    /// Stores and manages operations on multiple storage slots.
    /// </summary>
    /// <typeparam name="_BaseItem">The base item.</typeparam>
    /// <typeparam name="_Slot">The slot type.</typeparam>
    public class Inventory<_BaseItem, _Slot, _ItemInstance> : IInventory<_BaseItem, _Slot, _ItemInstance>
        where _BaseItem : class, IBaseItem
        where _ItemInstance : struct, IItemInstance<_BaseItem>
        where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
    {
        /// <summary>
        /// The slots within this inventory.
        /// </summary>
        public _Slot[] slots { get; set; }
        
        /// <summary>
        /// The remaining capacity of this inventory.
        /// </summary>
        public uint capacity
        {
            get
            {
                uint a = 0;
                foreach (_Slot s in slots)
                    if (s.itemInstance.isEmpty)
                        a += 1;
                return a;
            }
        }

        public Inventory(_ItemInstance[] PointerItems)
        {
            slots = new _Slot[PointerItems.Length];
            for (int i = 0; i < PointerItems.Length; i++)
            {
                slots[i] = new _Slot();
                slots[i].itemInstance = PointerItems[i];
            }
        }

        public virtual void AddItem(_ItemInstance other) => 
            InventoryManagementUtilities.AddItemToInventory<_BaseItem,_Slot,_ItemInstance,IInventory<_BaseItem,_Slot,_ItemInstance>>(this, other);
        public virtual void ContainsItem(_ItemInstance other) => 
            InventoryManagementUtilities.ContainsItem<_BaseItem, _Slot, _ItemInstance, IInventory<_BaseItem, _Slot, _ItemInstance>>(this, other);
    }
}
