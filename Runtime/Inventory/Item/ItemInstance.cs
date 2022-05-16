namespace SoulShard.InventorySystem
{
    /// <summary>
    /// An instance of an item.
    /// </summary>
    /// <typeparam name="_BaseItem">The "item prefab" to use as the base for this instance.</typeparam>
    [System.Serializable]
    public struct ItemInstance<_BaseItem> where _BaseItem : class, IBaseItem
    {
        /// <summary>
        /// The Base item this pointeritem is instanced off of.
        /// </summary>
        public _BaseItem item;

        /// <summary>
        /// The quantity of this item.
        /// </summary>
        public uint amount;

        /// <summary>
        /// Is this an empty item instance?
        /// </summary>
        public bool isEmpty
        {
            get => item == null;
        }

        #region Constructors
        public ItemInstance(_BaseItem item = null, uint amount = 0)
        {
            this.item = item;
            this.amount = amount;
        }

        public ItemInstance(ItemInstance<_BaseItem> I)
        {
            item = I.item;
            amount = I.amount;
        }
        #endregion
    }
}
