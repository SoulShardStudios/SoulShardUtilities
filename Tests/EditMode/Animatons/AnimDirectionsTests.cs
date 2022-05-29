using NUnit.Framework;
using SoulShard.Animations;
using UnityEngine;

namespace SoulShard.Tests.Animations
{
    public class AnimDirectionsTests
    {
        [Test]
        public void TestApplyName()
        {
            AnimDirections dirs = new AnimDirections();

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
            AnimDirections dirs = new AnimDirections();

            dirs.ApplyNameToAll("_");

            Assert.AreEqual(dirs.NameForDirCardinal(Vector2.down), "_Down");
            Assert.AreEqual(dirs.NameForDirCardinal(Vector2.up), "_Up");
            Assert.AreEqual(dirs.NameForDirCardinal(Vector2.left), "_Left");
            Assert.AreEqual(dirs.NameForDirCardinal(Vector2.right), "_Right");
            Assert.AreEqual(dirs.NameForDirCardinal(new Vector2(1, 1)), "_Up");
            Assert.AreEqual(dirs.NameForDirCardinal(new Vector2(1, -1)), "_Down");
        }

        [Test]
        public void TestNameForDir()
        {
            AnimDirections dirs = new AnimDirections();

            dirs.ApplyNameToAll("_");

            Assert.AreEqual(dirs.NameForDir(Vector2.down), "_Down");
            Assert.AreEqual(dirs.NameForDir(Vector2.up), "_Up");
            Assert.AreEqual(dirs.NameForDir(Vector2.left), "_Left");
            Assert.AreEqual(dirs.NameForDir(Vector2.right), "_Right");
            Assert.AreEqual(dirs.NameForDir(new Vector2(1, 1)), "_UpRight");
            Assert.AreEqual(dirs.NameForDir(new Vector2(1, -1)), "_DownRight");
            Assert.AreEqual(dirs.NameForDir(new Vector2(-1, 1)), "_UpLeft");
            Assert.AreEqual(dirs.NameForDir(new Vector2(-1, -1)), "_DownLeft");
        }

        [Test]
        public void TestClear()
        {
            AnimDirections dirs = new AnimDirections();

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
