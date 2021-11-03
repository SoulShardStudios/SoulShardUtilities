using System;
using UnityEngine;
namespace SoulShard.Utils
{
    public class Timer
    {
        #region Variables
        public event Action OnDone;
        float _maxCooldown;
        float _currentCooldown = 0;
        bool _done = true;
        public float MaxCooldown { get => _maxCooldown; set => _maxCooldown = value; }
        public float CurrentCooldown => _currentCooldown;
        public float CurrentCooldownPercent { get => CurrentCooldown / _maxCooldown; }
        public Timer(float maxCooldown) => _maxCooldown = maxCooldown;
        #endregion
        #region Time Update Methods
        // handles the incrementation of the timer in real time (run this in an update loop)
        public void HandleTimerUnscaled()
        {
            _currentCooldown -= Time.unscaledDeltaTime;
            if (_currentCooldown <= 0 && !_done)
            {
                OnDone?.Invoke();
                _done = true;
            }
        }
        // handles the incrementation of the timer in scald in game time (run this in an update loop)
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
        public void ForceDone() => _currentCooldown = -1;
        public bool IsDone() => _done;
        public void ForceDoneNoEffect()
        {
            _currentCooldown = -1;
            _done = true;
        }
        public void Reset()
        {
            _currentCooldown = _maxCooldown;
            _done = false;
        }
        #endregion
    }
}