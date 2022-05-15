namespace SoulShard.Inventory
{
    public static class InventoryManagementUtilities
    {
        public static ItemInstance<_BaseItem> AddUnstackableItemToInventory<_BaseItem, _Slot>(
            Inventory<_BaseItem, _Slot> inventory,
            ItemInstance<_BaseItem> other
        )
            where _BaseItem : class, IBaseItem
            where _Slot : class, ISlot<_BaseItem>, new()
        {
            if (other == null)
                return other;
            if (other.item == null)
                return other;
            if (inventory.capacity == 0)
                return other;

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].itemInstance.item == null)
                {
                    inventory.slots[i].itemInstance = other;
                    return null;
                }
            }
            return other;
        }

        public static ItemInstance<_BaseItem> AddStackableItemToInventory<_BaseItem, _Slot>(
            Inventory<_BaseItem, _Slot> inventory,
            ItemInstance<_BaseItem> other
        )
            where _BaseItem : class, IBaseItem
            where _Slot : class, ISlot<_BaseItem>, new()
        {
            if (other == null)
                return other;
            if (other.item == null)
                return other;
            if (inventory.capacity == 0)
                return other;

            uint maxStack = other.item.maxStackAmount;

            if (other.amount == maxStack)
                return AddUnstackableItemToInventory(inventory, other);

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                ItemInstance<_BaseItem> current = inventory.slots[i].itemInstance;
                if (current.item.name == other.item.name)
                {
                    if (current.amount == maxStack)
                        continue;
                    if (current.amount + other.amount < maxStack)
                    {
                        current.amount += other.amount;
                        return null;
                    }

                    other.amount -= maxStack - current.amount;
                    current.amount = maxStack;
                }
            }

            return other;
        }

        public static ItemInstance<_BaseItem> AddItemToInventory<_BaseItem, _Slot>(
            Inventory<_BaseItem, _Slot> inventory,
            ItemInstance<_BaseItem> other
        )
            where _BaseItem : class, IBaseItem
            where _Slot : class, ISlot<_BaseItem>, new()
        {
            if (!other.item.isStackable)
                return AddUnstackableItemToInventory(inventory, other);
            return AddStackableItemToInventory(inventory, other);
        }

        // checks if the inventory contains a specific item
        public static bool ContainsItem<_BaseItem, _Slot>(
            Inventory<_BaseItem, _Slot> inventory,
            ItemInstance<_BaseItem> other
        )
            where _BaseItem : class, IBaseItem
            where _Slot : class, ISlot<_BaseItem>, new()
        {
            if (other == null)
                return false;
            if (other.item == null)
                return false;

            if (other.amount > 0)
                foreach (_Slot s in inventory.slots)
                    if (s.itemInstance != null && s.itemInstance.item != null)
                        if (s.itemInstance.item.name == other.item.name)
                            if (s.itemInstance.item.isStackable)
                            {
                                if (s.itemInstance.amount == other.amount)
                                    return true;
                            }
                            else
                                return true;
            return false;
        }
    }
}
