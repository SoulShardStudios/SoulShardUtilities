namespace SoulShard.InventorySystem
{
    public struct InventoryManagementUtilities
    {
        #region Add
        public static _ItemInstance AddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (other.isEmpty)
                return other;
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].isEmpty)
                {
                    inventory.slots[i].itemInstance = other;
                    return new _ItemInstance();
                }
            }
            return other;
        }

        public static _ItemInstance AddStackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (other.isEmpty)
                return other;

            uint maxStack = other.item.maxStackAmount;

            if (other.amount == maxStack)
                return AddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);

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
                        _ItemInstance newItem = inventory.slots[i].itemInstance;
                        newItem.amount += other.amount;
                        inventory.slots[i].itemInstance = newItem;
                        return new _ItemInstance();
                    }

                    other.amount -= maxStack - inventory.slots[i].itemInstance.amount;
                    _ItemInstance newItem2 = inventory.slots[i].itemInstance;
                    newItem2.amount = maxStack;
                    inventory.slots[i].itemInstance = newItem2;
                }
            }
            if (other.amount == 0)
                return new _ItemInstance();
            if (other.amount > 0)
                return AddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);

            return other;
        }

        public static _ItemInstance AddItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (other.isEmpty)
                return other;
            if (!other.item.isStackable)
                return AddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);
            return AddStackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);
        }
        #endregion
        #region Contains
        // checks if the inventory contains a specific item
        public static bool ContainsItem<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (other.isEmpty)
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
        #endregion
        #region Can Add
        public static bool CanAddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (other.isEmpty)
                return false;
            for (int i = 0; i < inventory.slots.Length; i++)
                if (inventory.slots[i].isEmpty)
                    return true;
            return false;
        }

        public static bool CanAddStackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (other.isEmpty)
                return false;

            uint maxStack = other.item.maxStackAmount;
            if (other.amount == maxStack)
                return CanAddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].itemInstance.isEmpty)
                    continue;
                if (inventory.slots[i].itemInstance.item.name == other.item.name)
                {
                    if (inventory.slots[i].itemInstance.amount == maxStack)
                        continue;
                    if (inventory.slots[i].itemInstance.amount + other.amount <= maxStack)
                        return true;

                    other.amount -= maxStack - inventory.slots[i].itemInstance.amount;
                }
            }

            if (other.amount == 0)
                return true;
            if (other.amount > 0)
                return CanAddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);

            return false;
        }

        public static bool CanAddItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (other.isEmpty)
                return false;
            if (!other.item.isStackable)
                return CanAddUnstackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);
            return CanAddStackableItemToInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, other);
        }
        #endregion
        #region Take Item
        public static _ItemInstance TakeItemFromInventoryUnstackable<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory, _BaseItem toTake
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            foreach (var slot in inventory.slots)
            {
                if (slot.itemInstance.isEmpty)
                    continue;
                if (slot.itemInstance.item.name == toTake.name)
                {
                    var original = slot.itemInstance;
                    slot.itemInstance = new _ItemInstance();
                    return original;
                }
            }
            return new _ItemInstance();
        }

        public static _ItemInstance TakeItemFromInventoryStackable<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory, _BaseItem toTake, uint amount
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (amount > toTake.maxStackAmount)
                throw new System.Exception("Should not take more than a stack's worth of items.");
            _ItemInstance result = new _ItemInstance();
            foreach (var slot in inventory.slots)
            {
                if (slot.isEmpty)
                    continue;
                if (slot.itemInstance.item.name != toTake.name)
                    continue;
                if (result.item == null)
                    result.item = toTake;
                if (result.amount + slot.itemInstance.amount < amount)
                {
                    result.amount += slot.itemInstance.amount;
                    slot.itemInstance = new _ItemInstance();
                }
                else
                {
                    slot.itemInstance = new _ItemInstance() 
                    { 
                        item = toTake,
                        amount = result.amount + slot.itemInstance.amount - amount
                    };
                    result.amount = amount;
                    return result;
                }
                if (result.amount == amount)
                    return result;
            }
            return result;
        }

        public static _ItemInstance TakeItemFromInventory<_BaseItem, _Slot, _ItemInstance, _Inventory>(
            _Inventory inventory, _BaseItem toTake, uint amount
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
            where _Slot : class, ISlot<_BaseItem, _ItemInstance>, new()
            where _Inventory : class, IInventory<_BaseItem, _Slot, _ItemInstance>
        {
            if (toTake.isStackable)
                return TakeItemFromInventoryStackable<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, toTake, amount);
            else
                return TakeItemFromInventoryUnstackable<_BaseItem, _Slot, _ItemInstance, _Inventory>(inventory, toTake);
        }
        #endregion
        }
}
