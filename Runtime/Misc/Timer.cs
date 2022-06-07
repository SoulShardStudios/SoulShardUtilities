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
        public bool done { get; private set; }

        /// <summary>
        /// Where the cooldown gets reset to.
        /// </summary>
        public float maxCooldown;

        /// <summary>
        /// Should this timer automatically reset when its done?
        /// </summary>
        public bool autoReset;
        public float currentCooldownPercent
        {
            get => Mathf.Clamp(currentCooldown, 0, maxCooldown) / maxCooldown;
        }

        public Timer(float maxCooldown, bool autoReset = false)
        {
            this.maxCooldown = maxCooldown;
            this.autoReset = autoReset;
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
            if (currentCooldown <= 0 && !done)
            {
                onDone?.Invoke();
                if (autoReset)
                {
                    currentCooldown = maxCooldown;
                    return;
                }
                done = true;
            }
        }

        /// <summary>
        /// Forces the timer to complete.
        /// </summary>
        public void ForceDone()
        {
            currentCooldown = -1;
            if (!autoReset)
                done = true;
            onDone?.Invoke();
        }

        /// <summary>
        /// Resets the timer to start again.
        /// </summary>
        public void Reset()
        {
            currentCooldown = maxCooldown;
            done = false;
        }
    }
}
