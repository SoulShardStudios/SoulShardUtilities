using NUnit.Framework;
using SoulShard.InventorySystem;

namespace SoulShard.Tests.InventorySystem
{
    class TestSlot
    {
        [Test]
        public void TestSlotTransfer()
        {
            Slot<BaseItem, ItemInstance<BaseItem>> slot = new Slot<BaseItem, ItemInstance<BaseItem>>(new ItemInstance<BaseItem>(Helpers.salt, 12));
            var res = slot.Transfer(new ItemInstance<BaseItem>(Helpers.AXE));
            Assert.AreEqual(res.amount, 12);
            Assert.AreEqual(res.item.name, Helpers.salt.name);
            Assert.AreEqual(slot.itemInstance.amount, 0);
            Assert.AreEqual(slot.itemInstance.item.name, Helpers.AXE.name);
        }

        [Test]
        public void TestOnItemModifed()
        {
            ItemInstance<BaseItem> res = new ItemInstance<BaseItem>();
            Slot<BaseItem, ItemInstance<BaseItem>> slot = new Slot<BaseItem, ItemInstance<BaseItem>>(new ItemInstance<BaseItem>(Helpers.salt, 12));
            slot.onItemModified = (ItemInstance<BaseItem> item) => res = item;
            var testTransfer = new ItemInstance<BaseItem>(Helpers.AXE);

            slot.Transfer(testTransfer);
            Assert.AreEqual(testTransfer.item.name, res.item.name);
            Assert.AreEqual(testTransfer.amount, res.amount);
            slot.itemInstance = new ItemInstance<BaseItem>();
            Assert.AreEqual(slot.itemInstance.item, res.item);
            Assert.AreEqual(slot.itemInstance.amount, res.amount);
        }
    }
}
