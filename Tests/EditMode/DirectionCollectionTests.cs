using NUnit.Framework;
using UnityEngine;
using SoulShard.Utils;

namespace SoulShard.Tests.Utils
{
    public class DirectionCollectionTests
    {
        [Test]
        public void TestApplyName()
        {
            StringDirections dirs = new StringDirections();

            dirs.ApplyNameToAll("HELLO_");

            Assert.AreEqual(dirs.name, "HELLO_");
            Assert.AreEqual(dirs.right, "HELLO_Right");
            Assert.AreEqual(dirs.left, "HELLO_Left");
            Assert.AreEqual(dirs.down, "HELLO_Down");
            Assert.AreEqual(dirs.up, "HELLO_Up");
            Assert.AreEqual(dirs.downRight, "HELLO_DownRight");
            Assert.AreEqual(dirs.upRight, "HELLO_UpRight");
            Assert.AreEqual(dirs.downLeft, "HELLO_DownLeft");
            Assert.AreEqual(dirs.upLeft, "HELLO_UpLeft");

            dirs.ApplyNameToAll("HAHAHA");

            Assert.AreEqual(dirs.name, "HELLO_");
            Assert.AreEqual(dirs.right, "HELLO_Right");
            Assert.AreEqual(dirs.left, "HELLO_Left");
            Assert.AreEqual(dirs.down, "HELLO_Down");
            Assert.AreEqual(dirs.up, "HELLO_Up");
            Assert.AreEqual(dirs.downRight, "HELLO_DownRight");
            Assert.AreEqual(dirs.upRight, "HELLO_UpRight");
            Assert.AreEqual(dirs.downLeft, "HELLO_DownLeft");
            Assert.AreEqual(dirs.upLeft, "HELLO_UpLeft");
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
    }
}
