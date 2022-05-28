using NUnit.Framework;
using SoulShard.InventorySystem;

namespace SoulShard.Tests.InventorySystem
{
    class TestSlot
    {
        [Test]
        public void TestSlotTransfer()
        {
            Slot<BaseItem, ItemInstance<BaseItem>> slot = new Slot<
                BaseItem,
                ItemInstance<BaseItem>
            >(new ItemInstance<BaseItem>(Helpers.salt, 12));
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
            Slot<BaseItem, ItemInstance<BaseItem>> slot = new Slot<
                BaseItem,
                ItemInstance<BaseItem>
            >(new ItemInstance<BaseItem>(Helpers.salt, 12));
            slot.onItemModified = (ItemInstance<BaseItem> item) => res = item;
            var testTransfer = new ItemInstance<BaseItem>(Helpers.AXE);

            slot.Transfer(testTransfer);
            Assert.AreEqual(testTransfer.item.name, res.item.name);
            Assert.AreEqual(testTransfer.amount, res.amount);
            slot.itemInstance = new ItemInstance<BaseItem>();
            Assert.AreEqual(slot.itemInstance.item, res.item);
            Assert.AreEqual(slot.itemInstance.amount, res.amount);
        }

        [Test]
        public void TestSwap() =>
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.Swap<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 1023),
                new ItemInstance<BaseItem>(Helpers.salt, 12)
            );
    }

    class TestCombineStack
    {
        [Test]
        public void TestCombineSimple()
        {
            var res = SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>(Helpers.salt, 10),
                new ItemInstance<BaseItem>(Helpers.salt, 10)
            );
            Assert.AreEqual(res.Item1.amount, 20);
            Assert.AreEqual(res.Item2.amount, 0);
            Assert.AreEqual(res.Item1.item, Helpers.salt);
            Assert.AreEqual(res.Item2.item, null);
        }

        [Test]
        public void TestOverflow()
        {
            var res = SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>(Helpers.salt, 70),
                new ItemInstance<BaseItem>(Helpers.salt, 80)
            );
            Assert.AreEqual(res.Item1.amount, 100);
            Assert.AreEqual(res.Item2.amount, 50);
            Assert.AreEqual(res.Item1.item, res.Item2.item);
            Assert.AreEqual(res.Item1.item, Helpers.salt);
        }

        [Test]
        public void TestEdgeCases()
        {
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(),
                new ItemInstance<BaseItem>(Helpers.salt, 80)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 80),
                new ItemInstance<BaseItem>()
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 80),
                new ItemInstance<BaseItem>(Helpers.AXE)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.witchesBrew, 10),
                new ItemInstance<BaseItem>(Helpers.salt, 80)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.AXE, 10),
                new ItemInstance<BaseItem>(Helpers.AXE, 80)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 100),
                new ItemInstance<BaseItem>(Helpers.salt, 20)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.CombineStack<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 20),
                new ItemInstance<BaseItem>(Helpers.salt, 100)
            );
        }
    }

    class TestHalfSplitStack
    {
        [Test]
        public void TestSimpleSplit()
        {
            var res = SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>(Helpers.salt, 10),
                new ItemInstance<BaseItem>(Helpers.salt, 2)
            );
            Assert.AreEqual(res.Item1.amount, 5);
            Assert.AreEqual(res.Item2.amount, 7);
            Assert.AreEqual(res.Item1.item, res.Item2.item);
            Assert.AreEqual(res.Item1.item, Helpers.salt);
        }

        [Test]
        public void TestUnevenSplit()
        {
            var res = SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>(Helpers.salt, 11),
                new ItemInstance<BaseItem>(Helpers.salt, 3)
            );
            Assert.AreEqual(res.Item1.amount, 5);
            Assert.AreEqual(res.Item2.amount, 9);
            Assert.AreEqual(res.Item1.item, res.Item2.item);
            Assert.AreEqual(res.Item1.item, Helpers.salt);
        }

        [Test]
        public void TestEdgeCases()
        {
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(),
                new ItemInstance<BaseItem>(Helpers.salt, 80)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(),
                new ItemInstance<BaseItem>(Helpers.AXE)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.AXE),
                new ItemInstance<BaseItem>()
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.witchesBrew, 20),
                new ItemInstance<BaseItem>(Helpers.salt, 80)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 80),
                new ItemInstance<BaseItem>(Helpers.witchesBrew, 20)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 1),
                new ItemInstance<BaseItem>()
            );
        }
    }

    class TestSIngleStackSplit
    {
        [Test]
        public void TestSimpleSpLit()
        {
            var res = SlotManagementFuncs.SingleStackSplit<BaseItem, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>(Helpers.salt, 20),
                new ItemInstance<BaseItem>(Helpers.salt, 3)
            );
            Assert.AreEqual(res.Item1.amount, 21);
            Assert.AreEqual(res.Item2.amount, 2);
            Assert.AreEqual(res.Item1.item, res.Item2.item);
            Assert.AreEqual(res.Item1.item, Helpers.salt);
        }

        [Test]
        public void TestCurrentIsEmpty()
        {
            var res = SlotManagementFuncs.SingleStackSplit<BaseItem, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>(),
                new ItemInstance<BaseItem>(Helpers.salt, 3)
            );
            Assert.AreEqual(res.Item1.amount, 1);
            Assert.AreEqual(res.Item2.amount, 2);
            Assert.AreEqual(res.Item1.item, res.Item2.item);
            Assert.AreEqual(res.Item1.item, Helpers.salt);
        }

        [Test]
        public void TestRemoveAtEnd()
        {
            var res = SlotManagementFuncs.SingleStackSplit<BaseItem, ItemInstance<BaseItem>>(
                new ItemInstance<BaseItem>(Helpers.salt, 20),
                new ItemInstance<BaseItem>(Helpers.salt, 1)
            );
            Assert.AreEqual(res.Item1.amount, 21);
            Assert.AreEqual(res.Item2.amount, 0);
            Assert.AreEqual(res.Item2.item, null);
            Assert.AreEqual(res.Item1.item, Helpers.salt);
        }

        [Test]
        public void TestEdgeCases()
        {
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 1),
                new ItemInstance<BaseItem>()
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(),
                new ItemInstance<BaseItem>(Helpers.salt, 1)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.AXE),
                new ItemInstance<BaseItem>()
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(),
                new ItemInstance<BaseItem>(Helpers.AXE)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.witchesBrew, 5),
                new ItemInstance<BaseItem>(Helpers.salt, 6)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(Helpers.salt, 6),
                new ItemInstance<BaseItem>(Helpers.witchesBrew, 5)
            );
            Helpers.AssertEqualToSwap(
                SlotManagementFuncs.HalfStackSplit<BaseItem, ItemInstance<BaseItem>>,
                new ItemInstance<BaseItem>(),
                new ItemInstance<BaseItem>(Helpers.salt, 100)
            );
        }
    }
}
