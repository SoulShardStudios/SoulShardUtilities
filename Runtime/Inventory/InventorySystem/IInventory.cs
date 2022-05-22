namespace SoulShard.InventorySystem
{
    public interface IInventory<_BaseItem, _Slot, _ItemInstance>
        where _BaseItem : class, IBaseItem
        where _ItemInstance : struct, IItemInstance<_BaseItem>
        where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
    {
        public _Slot[] slots { get; set; }
        public uint capacity { get; }
    }
}
