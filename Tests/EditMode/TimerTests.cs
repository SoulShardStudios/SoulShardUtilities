using SoulShard.Utils;
using NUnit.Framework;
using UnityEngine;
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
    public void TestOnCycleComplete()
    {
        bool done = false;
        var timer = new Timer(0.5f);
        timer.onCycleComplete += () => done = true;
        for (int i = 0; i < 55; i++)
            timer.Tick(0.05f);
        Assert.True(done);
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
    public void TestAutoResetInfinite()
    {
        int cycles = 0;
        int dones = 0;
        var timer = new Timer(0.5f, 0);
        timer.onDone += () => dones++;
        timer.onCycleComplete += () => cycles++;
        for (int i = 0; i < 10000; i++)
            timer.Tick(0.05f);
        Assert.True(cycles == 1000);
        Assert.True(dones == 0);
        Assert.True(!timer.done);
    }
    [Test]
    public void TestAutoResetFinite()
    {
        int cycles = 0;
        int dones = 0;
        var timer = new Timer(0.5f, 500);
        timer.onDone += () => dones++;
        timer.onCycleComplete += () => cycles++;
        for (int i = 0; i < 10000; i++)
            timer.Tick(0.05f);
        Assert.True(cycles == 500);
        Assert.True(dones == 1);
    }
    [Test]
    public void TestCompleteCycle()
    {
        var timer = new Timer(10, 1);
        timer.CompleteCycle();
        Assert.True(timer.done);
        var timer2 = new Timer(10, 2);
        timer2.CompleteCycle();
        timer2.CompleteCycle();
        Assert.True(timer2.done);
    }
}
