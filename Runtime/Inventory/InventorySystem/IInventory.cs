namespace SoulShard.Inventory
{
    public interface IInventory<_BaseItem, _Slot>
        where _BaseItem : class, IBaseItem
        where _Slot : class, ISlot<_BaseItem>, new()
    {
        public _Slot[] slots { get; set; }
    }
}
