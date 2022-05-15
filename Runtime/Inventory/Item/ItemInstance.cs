namespace SoulShard.Inventory
{
    /// <summary>
    /// An instance of an item.
    /// </summary>
    /// <typeparam name="BaseItem">The "item prefab" to use as the base for this instance.</typeparam>
    [System.Serializable]
    public class ItemInstance<BaseItem> where BaseItem : class, IBaseItem
    {
        /// <summary>
        /// The Base item this pointeritem is instanced off of.
        /// </summary>
        public BaseItem item;

        /// <summary>
        /// /// The quantity of this item.
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
        public ItemInstance(BaseItem item, uint amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public ItemInstance()
        {
            item = null;
            amount = 0;
        }

        public ItemInstance(ItemInstance<BaseItem> I)
        {
            item = I.item;
            amount = I.amount;
        }

        public ItemInstance(BaseItem item)
        {
            this.item = item;
            amount = 0;
        }
    #endregion
    }
}
