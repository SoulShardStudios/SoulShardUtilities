using SoulShard.InventorySystem;

namespace SoulShard.Tests.InventorySystem
{
    public static class Helpers
    {
        public static BaseItem salt = new BaseItem(
            isStackable: true,
            maxStackAmount: 100,
            name: "Salt"
        );
        public static readonly BaseItem witchesBrew = new BaseItem(name: "Witches Brew");
        public static readonly BaseItem AXE = new BaseItem(name: "AXXXXEEEEEE");
        public static readonly BaseItem weed = new BaseItem(
            name: "Weed",
            maxStackAmount: 100,
            isStackable: true
        );

        public static Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>> GetFullInventory() =>
            new Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>[1] { new ItemInstance<BaseItem>(witchesBrew) }
            );

        public static Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>> GetHoleyInventory() =>
            new Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>[10]
                {
                    new ItemInstance<BaseItem>(salt, 69),
                    new ItemInstance<BaseItem>(),
                    new ItemInstance<BaseItem>(),
                    new ItemInstance<BaseItem>(witchesBrew),
                    new ItemInstance<BaseItem>(),
                    new ItemInstance<BaseItem>(),
                    new ItemInstance<BaseItem>(),
                    new ItemInstance<BaseItem>(weed, 100), // in MG lol
                    new ItemInstance<BaseItem>(weed, 50),
                    new ItemInstance<BaseItem>()
                }
            );

        public static System.Func<
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            ItemInstance<BaseItem>
        >[] addItemFuncs() =>
            new System.Func<
                Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>,
                ItemInstance<BaseItem>,
                ItemInstance<BaseItem>
            >[3]
            {
                InventoryManagementUtilities.AddItemToInventory<BaseItem,Slot<BaseItem, ItemInstance<BaseItem>>,ItemInstance<BaseItem>,Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>>,
                InventoryManagementUtilities.AddStackableItemToInventory<BaseItem,Slot<BaseItem, ItemInstance<BaseItem>>,ItemInstance<BaseItem>,Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>>,
                InventoryManagementUtilities.AddUnstackableItemToInventory<BaseItem,Slot<BaseItem, ItemInstance<BaseItem>>,ItemInstance<BaseItem>,Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>>
            };
    }
}
