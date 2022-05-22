namespace SoulShard.InventorySystem
{
    public interface ISlot<_BaseItem, _ItemInstance> where _BaseItem : class, IBaseItem where _ItemInstance : struct, IItemInstance<_BaseItem>
    {
        /// <summary>
        /// The item instance this slot stores.
        /// </summary>
        public _ItemInstance itemInstance { get; set; }

        /// <summary>
        /// The function to invoke when an item is modified
        /// </summary>
        public System.Action<_ItemInstance> onItemModified { get; set; }

        /// <summary>
        /// Make a transfer between this slot and the hand.
        /// </summary>
        /// <param name="button">The mouse button pressed to make the transfer with</param>
        /// <param name="other">The pointeritem in the hand</param>
        /// <returns>The new item held by the hand.</returns>
        public _ItemInstance Transfer(_ItemInstance other, string button = "");

        /// <summary>
        /// Whether the slot is empty (thanks nullable types!!!!)
        /// </summary>
        public bool isEmpty { get; }
    }
}
