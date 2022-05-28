namespace SoulShard.InventorySystem
{
    /// <summary>
    /// An instance of an item.
    /// </summary>
    /// <typeparam name="_BaseItem">The "item prefab" to use as the base for this instance.</typeparam>
    [System.Serializable]
    public struct ItemInstance<_BaseItem> : IItemInstance<_BaseItem>
        where _BaseItem : class, IBaseItem
    {
        _BaseItem _item;
        uint _amount;
        public bool isEmpty
        {
            get => item == null;
        }
        public _BaseItem item
        {
            get => _item;
            set => _item = value;
        }
        public uint amount
        {
            get => _amount;
            set => _amount = value;
        }

        #region Constructors
        public ItemInstance(_BaseItem item = null, uint amount = 0)
        {
            _item = item;
            _amount = amount;
        }

        public ItemInstance(ItemInstance<_BaseItem> I)
        {
            _item = I.item;
            _amount = I.amount;
        }
        #endregion
    }
}
