using NUnit.Framework;
using UnityEngine;
using SoulShard.Utils;

namespace SoulShard.Tests.Utils
{
    public class DirectionCollectionTests
    {
        void AssertNameApplied(StringDirections dirs, string name)
        {
            Assert.AreEqual(dirs.name, name);
            Assert.AreEqual(dirs.right, $"{name}Right");
            Assert.AreEqual(dirs.left, $"{name}Left");
            Assert.AreEqual(dirs.down, $"{name}Down");
            Assert.AreEqual(dirs.up, $"{name}Up");
            Assert.AreEqual(dirs.downRight, $"{name}DownRight");
            Assert.AreEqual(dirs.upRight, $"{name}UpRight");
            Assert.AreEqual(dirs.downLeft, $"{name}DownLeft");
            Assert.AreEqual(dirs.upLeft, $"{name}UpLeft");
        }

        [Test]
        public void TestApplyByCardinal()
        {
            StringDirections dirs = new StringDirections();
            dirs.name = ":)";
            Assert.True(dirs.CompareCardinalDir(Vector2.down, ":)Down"));
            AssertNameApplied(dirs, ":)");
        }

        [Test]
        public void TestApplyByDir()
        {
            StringDirections dirs = new StringDirections();
            dirs.name = ":)";
            Assert.True(dirs.CompareDir(Vector2.down, ":)Down"));
            AssertNameApplied(dirs, ":)");
        }

        [Test]
        public void TestApplyName()
        {
            StringDirections dirs = new StringDirections();
            dirs.ApplyNameToAll("HELLO");
            AssertNameApplied(dirs, "HELLO");
            dirs.ApplyNameToAll("HAHAHA");
            AssertNameApplied(dirs, "HELLO");
        }

        [Test]
        public void TestNameForDirCardinal()
        {
            StringDirections dirs = new StringDirections();

            dirs.ApplyNameToAll("_");

            Assert.True(dirs.CompareCardinalDir(Vector2.down, "_Down"));
            Assert.True(dirs.CompareCardinalDir(Vector2.up, "_Up"));
            Assert.True(dirs.CompareCardinalDir(Vector2.left, "_Left"));
            Assert.True(dirs.CompareCardinalDir(Vector2.right, "_Right"));
            Assert.True(dirs.CompareCardinalDir(new Vector2(1, 1), "_Up"));
            Assert.True(dirs.CompareCardinalDir(new Vector2(1, -1), "_Down"));
        }

        [Test]
        public void TestNameForDir()
        {
            StringDirections dirs = new StringDirections();

            dirs.ApplyNameToAll("_");

            Assert.True(dirs.CompareDir(Vector2.down, "_Down"));
            Assert.True(dirs.CompareDir(Vector2.up, "_Up"));
            Assert.True(dirs.CompareDir(Vector2.left, "_Left"));
            Assert.True(dirs.CompareDir(Vector2.right, "_Right"));
            Assert.True(dirs.CompareDir(new Vector2(1, 1), "_UpRight"));
            Assert.True(dirs.CompareDir(new Vector2(1, -1), "_DownRight"));
            Assert.True(dirs.CompareDir(new Vector2(-1, 1), "_UpLeft"));
            Assert.True(dirs.CompareDir(new Vector2(-1, -1), "_DownLeft"));
        }

        [Test]
        public void TestClear()
        {
            StringDirections dirs = new StringDirections();

            dirs.ApplyNameToAll("_");
            dirs.Clear();

            Assert.AreEqual(dirs.name, "");
            Assert.AreEqual(dirs.right, "");
            Assert.AreEqual(dirs.left, "");
            Assert.AreEqual(dirs.down, "");
            Assert.AreEqual(dirs.up, "");
            Assert.AreEqual(dirs.downRight, "");
            Assert.AreEqual(dirs.upRight, "");
            Assert.AreEqual(dirs.downLeft, "");
            Assert.AreEqual(dirs.upLeft, "");
        }

        [Test]
        public void TestSetAll()
        {
            StringDirections dirs = new StringDirections();

            dirs.SetAll("_");

            Assert.AreEqual(dirs.right, "_");
            Assert.AreEqual(dirs.left, "_");
            Assert.AreEqual(dirs.down, "_");
            Assert.AreEqual(dirs.up, "_");
            Assert.AreEqual(dirs.downRight, "_");
            Assert.AreEqual(dirs.upRight, "_");
            Assert.AreEqual(dirs.downLeft, "_");
            Assert.AreEqual(dirs.upLeft, "_");
        }
    }
}
