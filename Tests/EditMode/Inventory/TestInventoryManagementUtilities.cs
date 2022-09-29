using NUnit.Framework;
using SoulShard.InventorySystem;
using UnityEngine;
public class TestInventorySystem
{
    #region Add
    [Test]
    public void TestAddInvalidItemToInventory()
    {
        var inven = Helpers.GetFullInventory();
        foreach (var fn in Helpers.AddItemFuncs())
        {
            var res = fn(inven, new ItemInstance<BaseItem>());
            Assert.AreEqual(res.item, null);
            Assert.AreEqual(res.amount, 0);
        }
    }

    [Test]
    public void TestAddUnstackableToInventory()
    {
        var inven = Helpers.GetHoleyInventory();
        InventoryManagementUtilities.AddUnstackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.witchesBrew));
        InventoryManagementUtilities.AddUnstackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.axe));
        InventoryManagementUtilities.AddUnstackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.axe));
        Assert.AreEqual(inven.slots[1].itemInstance.item.name, Helpers.witchesBrew.name);
        Assert.AreEqual(inven.slots[2].itemInstance.item.name, Helpers.axe.name);
        Assert.AreEqual(inven.slots[4].itemInstance.item.name, Helpers.axe.name);
    }

    [Test]
    public void TestAddStackableItemToInventory()
    {
        var inven = Helpers.GetHoleyInventory();
        InventoryManagementUtilities.AddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.salt, 10));
        Assert.AreEqual(inven.slots[0].itemInstance.amount, 79);
        Assert.AreEqual(inven.slots[0].itemInstance.item.name, Helpers.salt.name);
        InventoryManagementUtilities.AddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.salt, 30));
        Assert.AreEqual(inven.slots[0].itemInstance.amount, 100);
        Assert.AreEqual(inven.slots[0].itemInstance.item.name, Helpers.salt.name);
        Assert.AreEqual(inven.slots[1].itemInstance.item.name, Helpers.salt.name);
        Assert.AreEqual(inven.slots[1].itemInstance.amount, 9);
    }

    [Test]
    public void TestAddToInventoryWithStackableHoles()
    {
        var inven = Helpers.GetFullInventoryWithStackableHoles();
        InventoryManagementUtilities.AddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.salt, 10));
        Assert.AreEqual(inven.slots[0].itemInstance.amount, 35);
        Assert.AreEqual(inven.slots[0].itemInstance.item.name, Helpers.salt.name);
        InventoryManagementUtilities.AddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.salt, 30));
        Assert.AreEqual(inven.slots[0].itemInstance.amount, 65);
        Assert.AreEqual(inven.slots[0].itemInstance.item.name, Helpers.salt.name);
        var res = InventoryManagementUtilities.AddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.salt, 40));
        Assert.AreEqual(inven.slots[0].itemInstance.amount, 100);
        Assert.AreEqual(inven.slots[0].itemInstance.item.name, Helpers.salt.name);
        Assert.AreEqual(res.amount, 5);
        Assert.AreEqual(res.item.name, Helpers.salt.name);
        InventoryManagementUtilities.AddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.weed, 70));
        Assert.AreEqual(inven.slots[3].itemInstance.amount, 90);
        Assert.AreEqual(inven.slots[3].itemInstance.item.name, Helpers.weed.name);
    }
    #endregion
    #region Contains
    [Test]
    public void TestInventoryContainsItem()
    {
        var inven = Helpers.GetHoleyInventory();
        Assert.AreEqual(
            InventoryManagementUtilities.ContainsItem<
                BaseItem,
                Slot<BaseItem, ItemInstance<BaseItem>>,
                ItemInstance<BaseItem>,
                Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
            >(inven, new ItemInstance<BaseItem>(Helpers.salt, 69)),
            true
        );
        Assert.AreEqual(
            InventoryManagementUtilities.ContainsItem<
                BaseItem,
                Slot<BaseItem, ItemInstance<BaseItem>>,
                ItemInstance<BaseItem>,
                Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
            >(inven, new ItemInstance<BaseItem>(Helpers.salt, 39)),
            false
        );
        Assert.AreEqual(
            InventoryManagementUtilities.ContainsItem<
                BaseItem,
                Slot<BaseItem, ItemInstance<BaseItem>>,
                ItemInstance<BaseItem>,
                Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
            >(inven, new ItemInstance<BaseItem>(Helpers.axe, 0)),
            false
        );
        Assert.AreEqual(
            InventoryManagementUtilities.ContainsItem<
                BaseItem,
                Slot<BaseItem, ItemInstance<BaseItem>>,
                ItemInstance<BaseItem>,
                Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
            >(inven, new ItemInstance<BaseItem>(Helpers.witchesBrew, 0)),
            true
        );
    }
    #endregion
    #region CanAdd
    [Test]
    public void TestCanAddInvalidItemToInventory()
    {
        var inven = Helpers.GetHoleyInventory();
        foreach (var fn in Helpers.CanAddItemFuncs())
            Assert.False(fn(inven, new ItemInstance<BaseItem>()));
    }

    [Test]
    public void TestCanAddToFullInventory()
    {
        var inven = Helpers.GetFullInventory();
        foreach (var fn in Helpers.CanAddItemFuncs())
            Assert.False(fn(inven, new ItemInstance<BaseItem>(Helpers.witchesBrew)));
    }

    [Test]
    public void TestCanAddToInventory()
    {
        var inven = Helpers.GetHoleyInventory();
        foreach (var fn in Helpers.CanAddItemFuncs())
            Assert.True(fn(inven, new ItemInstance<BaseItem>(Helpers.witchesBrew)));
    }

    [Test]
    public void TestCanAddToInventoryWithStackableHoles()
    {
        var inven = Helpers.GetFullInventoryWithStackableHoles();

        var res = InventoryManagementUtilities.CanAddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.axe));
        Assert.False(res);

        var res2 = InventoryManagementUtilities.CanAddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.salt, 10));
        Assert.True(res2);

        var res3 = InventoryManagementUtilities.CanAddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.witchesBrew));
        Assert.False(res3);

        var res4 = InventoryManagementUtilities.CanAddStackableItemToInventory<
            BaseItem,
            Slot<BaseItem, ItemInstance<BaseItem>>,
            ItemInstance<BaseItem>,
            Inventory<BaseItem, Slot<BaseItem, ItemInstance<BaseItem>>, ItemInstance<BaseItem>>
        >(inven, new ItemInstance<BaseItem>(Helpers.weed, 10));
        Assert.True(res4);
    }
    #endregion
}
