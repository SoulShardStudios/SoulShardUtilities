using SoulShard.Utils;
using NUnit.Framework;

namespace SoulShard.Tests
{
    public class TimerTests
    {
        [Test]
        public void TestRegular()
        {
            var timer = new Timer(0.5f);
            Assert.True(timer.maxCooldown == 0.5f);
            Assert.True(timer.currentCooldown == 0.5f);
            Assert.True(timer.currentCooldownPercent == 1f);
            Assert.True(!timer.done);
            for (int i = 0; i < 5; i++)
                timer.Tick(0.05f);
            Assert.True(timer.maxCooldown == 0.5f);
            Assert.True(timer.currentCooldown < 0.251f);
            Assert.True(timer.currentCooldown > 0.249f);
            Assert.True(timer.currentCooldownPercent < 0.51f);
            Assert.True(timer.currentCooldownPercent > 0.49f);
            Assert.True(!timer.done);
            for (int i = 0; i < 5; i++)
                timer.Tick(0.05f);
            Assert.True(timer.maxCooldown == 0.5f);
            Assert.True(timer.currentCooldown <= 0.0f);
            Assert.True(timer.done);
            Assert.True(timer.currentCooldownPercent == 0);
        }

        [Test]
        public void TestOnDone()
        {
            bool done = false;
            var timer = new Timer(0.5f);
            timer.onDone += () => done = true;
            for (int i = 0; i < 55; i++)
                timer.Tick(0.05f);
            Assert.True(done);
        }

        [Test]
        public void TestAutoReset()
        {
            int calls = 0;
            var timer = new Timer(0.5f);
            timer.autoReset = true;
            timer.onDone += () => calls++;
            for (int i = 0; i < 10000; i++)
                timer.Tick(0.05f);
            Assert.True(calls == 1000);
        }
    }
}
