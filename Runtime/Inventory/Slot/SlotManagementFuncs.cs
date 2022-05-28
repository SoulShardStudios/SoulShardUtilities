using System;

namespace SoulShard.InventorySystem
{
    /// <summary>
    /// All functions within are slot transfer functions that return the slots item, and the other slots item respectively.
    /// </summary>
    public struct SlotManagementFuncs
    {
        public static (_ItemInstance, _ItemInstance) Swap<_BaseItem, _ItemInstance>(
            _ItemInstance current,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
        {
            _ItemInstance originalItem = current;
            current = other;
            return (current, originalItem);
        }

        public static (_ItemInstance, _ItemInstance) CombineStack<_BaseItem, _ItemInstance>(
            _ItemInstance current,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
        {
            if (current.isEmpty || other.isEmpty)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (current.item != other.item)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (!current.item.isStackable)
                return Swap<_BaseItem, _ItemInstance>(current, other);

            uint stackSize = other.item.maxStackAmount;

            if (current.amount >= stackSize || other.amount >= stackSize)
                return Swap<_BaseItem, _ItemInstance>(current, other);

            if (other.amount + current.amount < stackSize)
                return (
                    new _ItemInstance()
                    {
                        item = current.item,
                        amount = current.amount + other.amount
                    },
                    new _ItemInstance()
                );

            uint leftOver = current.amount + other.amount - stackSize;
            return (
                new _ItemInstance() { item = current.item, amount = stackSize },
                new _ItemInstance() { item = current.item, amount = leftOver }
            );
        }

        public static (_ItemInstance, _ItemInstance) HalfStackSplit<_BaseItem, _ItemInstance>(
            _ItemInstance current,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
        {
            if (current.isEmpty)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (!other.isEmpty ? current.item != other.item : false)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (!current.item.isStackable)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (current.amount < 2)
                return Swap<_BaseItem, _ItemInstance>(current, other);

            uint halfStack = (uint)Math.Floor((float)current.amount / 2);
            return (
                new _ItemInstance() { item = current.item, amount = halfStack },
                new _ItemInstance()
                {
                    item = current.item,
                    amount = other.amount + halfStack + (current.amount % 2)
                }
            );
        }

        public static (_ItemInstance, _ItemInstance) SingleStackSplit<_BaseItem, _ItemInstance>(
            _ItemInstance current,
            _ItemInstance other
        )
            where _BaseItem : class, IBaseItem
            where _ItemInstance : struct, IItemInstance<_BaseItem>
        {
            if (other.isEmpty)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (!other.item.isStackable)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (other.amount < 2)
                return (
                    new _ItemInstance()
                    {
                        item = other.item,
                        amount = current.amount + other.amount
                    },
                    new _ItemInstance()
                );
            if (current.isEmpty)
                return (
                    new _ItemInstance() { item = other.item, amount = 1 },
                    new _ItemInstance() { item = other.item, amount = other.amount - 1 }
                );
            if (current.item != other.item)
                return Swap<_BaseItem, _ItemInstance>(current, other);
            if (current.amount == current.item.maxStackAmount)
                return Swap<_BaseItem, _ItemInstance>(current, other);

            return (
                new _ItemInstance() { item = other.item, amount = current.amount + 1 },
                new _ItemInstance() { item = other.item, amount = other.amount - 1 }
            );
        }
    }
}
