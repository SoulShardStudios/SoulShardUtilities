namespace SoulShard.Inventory
{
    public class Inventory<_BaseItem, _Slot> : IInventory<_BaseItem, _Slot>
        where _BaseItem : class, IBaseItem
        where _Slot : class, ISlot<_BaseItem>, new()
    {
        public _Slot[] slots { get; set; }
        public uint capacity
        {
            get
            {
                uint a = 0;
                foreach (_Slot S in slots)
                    if (S.itemInstance.isEmpty)
                        a += 1;
                return a;
            }
        }

        public Inventory(ItemInstance<_BaseItem>[] PointerItems)
        {
            slots = new _Slot[PointerItems.Length];
            for (int i = 0; i < PointerItems.Length; i++)
            {
                slots[i] = new _Slot();
                slots[i].itemInstance = PointerItems[i];
            }
        }
    }
}
