using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using SoulShard.Utils;
using NUnit.Framework;

namespace SoulShard.Tests
{
    public class TimerTests
    {
        [UnityTest]
        public IEnumerator TestTimerUnscaled()
        {
            Application.targetFrameRate = 60;
            var timer = new Timer(0.5f);

            bool onDoneCalled = false;
            timer.onDone += () =>
            {
                onDoneCalled = true;
            };

            Assert.True(timer.maxCooldown == 0.5f);
            Assert.True(timer.currentCooldown == 0.5f);
            Assert.True(timer.currentCooldownPercent == 1f);
            Assert.True(!timer.done);
            Assert.True(!onDoneCalled);
            for (int i = 0; i < 18; i++)
            {
                timer.HandleTimerUnscaled();
                yield return new WaitForEndOfFrame();
            }
            Assert.True(timer.maxCooldown == 0.5f);
            Assert.True(timer.currentCooldown <= 0.26f);
            Assert.True(timer.currentCooldownPercent > 0.4f);
            Assert.True(timer.currentCooldownPercent < 0.6f);
            Assert.True(!timer.done);
            Assert.True(!onDoneCalled);
            for (int i = 0; i < 15; i++)
            {
                timer.HandleTimerUnscaled();
                yield return new WaitForEndOfFrame();
            }
            Assert.True(timer.maxCooldown == 0.5f);
            Assert.True(timer.currentCooldown <= 0.0f);
            Assert.True(timer.done);
            Assert.True(timer.currentCooldownPercent == 0);
            Assert.True(onDoneCalled);
        }

        [UnityTest]
        public IEnumerator TestTimerScaled()
        {
            Application.targetFrameRate = 60;
            Time.timeScale = 5;
            var timer = new Timer(5);

            bool onDoneCalled = false;
            timer.onDone += () =>
            {
                onDoneCalled = true;
            };

            Assert.True(timer.maxCooldown == 5f);
            Assert.True(timer.currentCooldown == 5f);
            Assert.True(timer.currentCooldownPercent == 1f);
            Assert.True(!timer.done);
            Assert.True(!onDoneCalled);
            for (int i = 0; i < 35; i++)
            {
                timer.HandleTimerScaled();
                yield return new WaitForEndOfFrame();
            }
            Assert.True(timer.maxCooldown == 5f);
            Assert.True(timer.currentCooldown <= 2.6f);
            Assert.True(timer.currentCooldownPercent > .4f);
            Assert.True(timer.currentCooldownPercent < .6f);
            Assert.True(!timer.done);
            Assert.True(!onDoneCalled);
            for (int i = 0; i < 35; i++)
            {
                timer.HandleTimerScaled();
                yield return new WaitForEndOfFrame();
            }
            Assert.True(timer.maxCooldown == 5f);
            Assert.True(timer.currentCooldown <= 0.0f);
            Assert.True(timer.done);
            Assert.True(timer.currentCooldownPercent == 0);
            Assert.True(onDoneCalled);
        }
    }
}
