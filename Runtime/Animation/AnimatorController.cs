using UnityEngine;
namespace SoulShard.Animations
{
    /// <summary>
    /// A simple animator controller, this allows for better 2D animation control.
    /// </summary>
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        string _currentState;
        /// <summary>
        /// Sets the current animation state to the state specified.
        /// </summary>
        /// <param name="state">The state to change to.</param>
        protected void ChangeAnimState(string state)
        {
            if (_currentState == state)
                return;
            _animator.Play(state);
            _currentState = state;
        }
    }
}