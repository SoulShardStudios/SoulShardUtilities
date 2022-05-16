using System;
using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// a timer stored in a variable, with some extra functionality
    /// </summary>
    public class Timer
    {
        #region Variables
        /// <summary>
        /// the action to call when the timer is done
        /// </summary>
        public event Action OnDone;

        /// <summary>
        /// the maximum cooldown time
        /// </summary>
        float _maxCooldown;

        /// <summary>
        /// the current cooldown time
        /// </summary>
        float _currentCooldown = 0;

        /// <summary>
        /// is the timer done?
        /// </summary>
        bool _done = true;
        public float MaxCooldown
        {
            get => _maxCooldown;
            set => _maxCooldown = value;
        }
        public float CurrentCooldown => _currentCooldown;
        public float CurrentCooldownPercent
        {
            get => CurrentCooldown / _maxCooldown;
        }

        public Timer(float maxCooldown) => _maxCooldown = maxCooldown;
        #endregion
        #region Time Update Methods
        /// <summary>
        /// handles the incrementation of the timer in real time (run this in an update loop)
        /// </summary>
        public void HandleTimerUnscaled()
        {
            _currentCooldown -= Time.unscaledDeltaTime;
            if (_currentCooldown <= 0 && !_done)
            {
                OnDone?.Invoke();
                _done = true;
            }
        }

        /// <summary>
        /// handles the incrementation of the timer in scald in game time (run this in an update loop)
        /// </summary>
        public void HandleTimerScaled()
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0 && !_done)
            {
                OnDone?.Invoke();
                _done = true;
            }
        }
        #endregion
        #region Cooldown Related Methods
        /// <summary>
        /// forces the timer to complete
        /// </summary>
        public void ForceDone() => _currentCooldown = -1;

        /// <summary>
        /// is the timer done?
        /// </summary>
        /// <returns>the respective boolean</returns>
        public bool IsDone() => _done;

        /// <summary>
        /// forces the timer to complete immediately
        /// </summary>
        public void ForceDoneNoEffect()
        {
            _currentCooldown = -1;
            _done = true;
        }

        /// <summary>
        /// resets the timer to start again
        /// </summary>
        public void Reset()
        {
            _currentCooldown = _maxCooldown;
            _done = false;
        }
        #endregion
    }
}
