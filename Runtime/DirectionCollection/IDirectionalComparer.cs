using System;
using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// Generic implementation for comparing a value to a value associated with the given direction.
    /// </summary>
    /// <typeparam name="T">The type of thing that's being compared or associated with a direction</typeparam>
    public interface IDirectionalComparer<T> where T : IEquatable<T>
    {
        /// <summary>
        /// For the given direction, compare the value associated with that direction with the given value.
        /// </summary>
        /// <param name="dir">The direction to compare with.</param>
        /// <param name="value">The value that direction should match.</param>
        bool CompareDir(Vector2 dir, T value);

        /// <summary>
        /// Gets the value associated with that direction.
        /// </summary>
        /// <param name="dir">The direction to get the value of</param>
        /// <returns>The value associated with that direction.</returns>
        T GetDir(Vector2 dir);
    }
}
