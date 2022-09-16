using System;
using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// A more concise way to track time versus a coroutine for simple cooldowns.
    /// </summary>
    [Serializable]
    public class Timer
    {
        /// <summary>
        /// The action to call when the timer has completed a cycle
        /// </summary>
        public event Action onCycleComplete;

        /// <summary>
        /// The action to call when the timer is done.
        /// </summary>
        public event Action onDone;

        /// <summary>
        /// The current cooldown time.
        /// </summary>
        public float currentCooldown { get; private set; }

        /// <summary>
        /// Is the timer done?
        /// </summary>
        public bool done => resetCount > 0 ? _numberOfResets >= resetCount : false;
        bool _cycleComplete => currentCooldown <= 0;

        /// <summary>
        /// Where the cooldown gets reset to.
        /// </summary>
        public float maxCooldown;

        /// <summary>
        /// The number of times the timer should reset.
        /// </summary>
        public readonly int resetCount;

        int _numberOfResets;
        public int numberOfResets { get => _numberOfResets; }
        
        /// <summary>
        /// The current percentage of the timer's completion
        /// </summary>
        public float currentCooldownPercent
        {
            get => Mathf.Clamp(currentCooldown, 0, maxCooldown) / maxCooldown;
        }

        public Timer(float maxCooldown, int resetCount = 1)
        {
            this.maxCooldown = maxCooldown;
            this.resetCount = resetCount;
            Reset();
        }

        /// <summary>
        /// Handles the incrementation of time.
        /// </summary>
        /// <param name="delta">The amount of time that has passed since the last Tick.</param>
        public void Tick(float delta)
        {
            if (done)
                return;
            currentCooldown -= delta;
            if (!_cycleComplete)
                return;
            CompleteCycle();
        }

        /// <summary>
        /// Forces the timer to complete its current cycle.
        /// </summary>
        public void CompleteCycle()
        {
            onCycleComplete?.Invoke();
            _numberOfResets++;
            if (!done)
                currentCooldown = maxCooldown;
            else
                onDone?.Invoke();
        }

        /// <summary>
        /// Resets the timer to start again.
        /// </summary>
        public void Reset()
        {
            _numberOfResets = 0;
            currentCooldown = maxCooldown;
        }
    }
}
