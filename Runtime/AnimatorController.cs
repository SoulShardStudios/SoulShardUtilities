using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// a simple animator controller, this allows for better 2D animation control.
    /// </summary>
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        string _currentState;
        protected void ChangeAnimState(string state)
        {
            if (_currentState == state)
                return;
            _animator.Play(state);
            _currentState = state;
        }
    }
}