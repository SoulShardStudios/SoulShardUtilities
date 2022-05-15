namespace SoulShard.Inventory
{
    /// <summary>
    /// Methods and properties required to be implemented for a base item to be a part of the built in inventory system.
    /// </summary>
    public interface IBaseItem
    {
        /// <summary>
        /// Is this item stackable?
        /// </summary>
        public bool isStackable { get; }

        /// <summary>
        /// If the item is stackable, what is its maximum stack amount?
        /// </summary>
        public uint maxStackAmount { get; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string name { get; }
    }
}
