namespace SoulShard.InventorySystem
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
            if (other.isEmpty)
                return other;
            if (inventory.capacity == 0)
                return other;

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].isEmpty)
                {
                    inventory.slots[i].itemInstance = other;
                    return new ItemInstance<_BaseItem>();
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
            if (other.isEmpty)
                return other;
            if (inventory.capacity == 0)
                return other;

            uint maxStack = other.item.maxStackAmount;

            if (other.amount == maxStack)
                return AddUnstackableItemToInventory(inventory, other);

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].itemInstance.isEmpty)
                    continue;
                if (inventory.slots[i].itemInstance.item.name == other.item.name)
                {
                    if (inventory.slots[i].itemInstance.amount == maxStack)
                        continue;
                    if (inventory.slots[i].itemInstance.amount + other.amount < maxStack)
                    {
                        ItemInstance<_BaseItem> newItem = inventory.slots[i].itemInstance;
                        newItem.amount += other.amount;
                        inventory.slots[i].itemInstance = newItem;
                        return new ItemInstance<_BaseItem>();
                    }

                    other.amount -= maxStack - inventory.slots[i].itemInstance.amount;
                    ItemInstance<_BaseItem> newItem2 = inventory.slots[i].itemInstance;
                    newItem2.amount = maxStack;
                    inventory.slots[i].itemInstance = newItem2;
                }
            }

            if (other.amount > 0)
                return AddUnstackableItemToInventory(inventory, other);

            return other;
        }

        public static ItemInstance<_BaseItem> AddItemToInventory<_BaseItem, _Slot>(
            Inventory<_BaseItem, _Slot> inventory,
            ItemInstance<_BaseItem> other
        )
            where _BaseItem : class, IBaseItem
            where _Slot : class, ISlot<_BaseItem>, new()
        {
            if (other.isEmpty)
                return other;
            if (inventory.capacity == 0)
                return other;

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
            if (other.isEmpty)
                return false;
            if (inventory.capacity == 0)
                return false;
            foreach (_Slot s in inventory.slots)
                if (!s.isEmpty)
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
