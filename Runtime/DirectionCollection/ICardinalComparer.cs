using System;
using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// Generic implementation for comparing a value to a value associated with a cardinal direction.
    /// </summary>
    /// <typeparam name="T">The type of thing that's being compared or associated with a cardinal direction</typeparam>
    public interface ICardinalComparer<T> where T : IEquatable<T>
    {
        /// <summary>
        /// For the given cardinal direction, compare the value associated with that cardinal direction with the given value.
        /// </summary>
        /// <param name="dir">The cardinal direction to compare with.</param>
        /// <param name="value">The value that direction should match.</param>
        bool CompareCardinalDir(Vector2 dir, T value);

        /// <summary>
        /// Gets the value associated with that cardinal direction.
        /// </summary>
        /// <param name="dir">The cardinal direction to get the value of</param>
        /// <returns>The value associated with that cardinal direction.</returns>
        T GetCardinalDir(Vector2 dir);
    }
}
