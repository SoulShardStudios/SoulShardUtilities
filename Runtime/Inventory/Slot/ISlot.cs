namespace SoulShard.Inventory
{
    /// <summary>
    /// Methods required for a slot to interface with the rest of the inventory system.
    /// </summary>
    /// <typeparam name="BaseItem">The base item the slot implementing this stores. </typeparam>
    public interface ISlot<BaseItem> where BaseItem : class, IBaseItem
    {
        /// <summary>
        /// The item instance this slot stores.
        /// </summary>
        public ItemInstance<BaseItem> itemInstance { get; set; }

        /// <summary>
        /// The function to invoke when an item is modified
        /// </summary>
        public System.Action<ItemInstance<BaseItem>> onItemModified { get; set; }

        /// <summary>
        /// Make a transfer between this slot and the hand.
        /// </summary>
        /// <param name="button">The mouse button pressed to make the transfer with</param>
        /// <param name="other">The pointeritem in the hand</param>
        /// <returns>The new item held by the hand.</returns>
        public ItemInstance<BaseItem> Transfer(string button, ItemInstance<BaseItem> other);
    }
}
