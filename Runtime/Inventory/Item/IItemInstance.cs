namespace SoulShard.InventorySystem
{
    /// <summary>
    /// An instance of an item.
    /// </summary>
    /// <typeparam name="_BaseItem">The "item prefab" to use as the base for this instance.</typeparam>

    public interface IItemInstance<_BaseItem> where _BaseItem : class, IBaseItem
    {
        /// <summary>
        /// The Base item this pointeritem is instanced off of.
        /// </summary>
        public _BaseItem item { get; set; }

        /// <summary>
        /// The quantity of this item.
        /// </summary>
        public uint amount { get; set; }

        /// <summary>
        /// Is this an empty item instance?
        /// </summary>
        public bool isEmpty { get; }
    }
}
