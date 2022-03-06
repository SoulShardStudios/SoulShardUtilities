using UnityEngine;

namespace SoulShard.Animations
{
    [System.Serializable]
    /// <summary>
    /// Allows for the easy management of a four direction animations names.
    /// </summary>
    public class FourDirectionAnim
    {
        /// <summary>
        /// Applies a name to all animations, with the name of the direction appended afterwards.
        /// </summary>
        public string name;
        /// <summary>
        /// Set the names of the animations you want to use exactly with this.
        /// </summary>
        public AnimDirections animDirs;
        /// <summary>
        /// Gets the animation name for the specified direction.
        /// </summary>
        /// <param name="direction">The direction to get the animation name for.</param>
        /// <returns>The name of the animation.</returns>
        public string nameForDir(Vector2 direction)
        {
            animDirs.ApplyNameToAll(name);
            if (direction == Vector2.zero)
                return animDirs.down;
            else
                return direction.y == 0 ? direction.x > 0 ? animDirs.right : animDirs.left : direction.y > 0 ? animDirs.up : animDirs.down;
        }
    }
}