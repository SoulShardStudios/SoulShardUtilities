using System;
using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// A more concise way to track time versus a coroutine for simple cooldowns.
    /// </summary>
    public class Timer
    {
        #region Variables
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
        public float currentCooldownPercent
        {
            get => Mathf.Clamp(currentCooldown, 0, maxCooldown) / maxCooldown;
        }

        public Timer(float maxCooldown)
        {
            this.maxCooldown = maxCooldown;
            Reset();
        }
        #endregion
        #region Time Update Methods
        /// <summary>
        /// Handles the incrementation of the timer in real time (run this in an update loop).
        /// </summary>
        public void HandleTimerUnscaled()
        {
            currentCooldown -= Time.unscaledDeltaTime;
            if (currentCooldown <= 0 && !done)
            {
                onDone?.Invoke();
                done = true;
            }
        }

        /// <summary>
        /// Handles the incrementation of the timer in scald in game time (run this in an update loop).
        /// </summary>
        public void HandleTimerScaled()
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0 && !done)
            {
                onDone?.Invoke();
                done = true;
            }
        }
        #endregion
        #region Cooldown Related Methods
        /// <summary>
        /// Forces the timer to complete.
        /// </summary>
        public void ForceDone()
        {
            currentCooldown = -1;
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
        #endregion
    }
}
