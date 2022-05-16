namespace SoulShard.InventorySystem
{
    public interface ISlot<_BaseItem> where _BaseItem : class, IBaseItem
    {
        /// <summary>
        /// The item instance this slot stores.
        /// </summary>
        public ItemInstance<_BaseItem> itemInstance { get; set; }

        /// <summary>
        /// The function to invoke when an item is modified
        /// </summary>
        public System.Action<ItemInstance<_BaseItem>> onItemModified { get; set; }

        /// <summary>
        /// Make a transfer between this slot and the hand.
        /// </summary>
        /// <param name="button">The mouse button pressed to make the transfer with</param>
        /// <param name="other">The pointeritem in the hand</param>
        /// <returns>The new item held by the hand.</returns>
        public ItemInstance<_BaseItem> Transfer(ItemInstance<_BaseItem> other, string button = "");

        /// <summary>
        /// Whether the slot is empty (thanks nullable types!!!!)
        /// </summary>
        public bool isEmpty { get; }
    }
}
