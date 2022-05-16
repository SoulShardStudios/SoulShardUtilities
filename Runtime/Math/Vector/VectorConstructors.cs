using UnityEngine;

namespace SoulShard.Math
{
    public partial struct VectorMath
    {
        /// <summary>
        /// A non native constructor for vectors
        /// </summary>
        /// <param name="value">The value to initialize all of the vector components with</param>
        /// <returns>A new vector</returns>
        public static Vector2 ConstructVector2(float value) => new Vector2(value, value);

        /// <summary>
        /// A non native constructor for vectors
        /// </summary>
        /// <param name="value">The value to initialize all of the vector components with</param>
        /// <returns>A new vector</returns>
        public static Vector2Int ConstructVector2(int value) => new Vector2Int(value, value);

        /// <summary>
        /// A non native constructor for vectors
        /// </summary>
        /// <param name="value">The value to initialize all of the vector components with</param>
        /// <returns>A new vector</returns>
        public static Vector3 ConstructVector3(float value) => new Vector3(value, value, value);

        /// <summary>
        /// A non native constructor for vectors
        /// </summary>
        /// <param name="value">The value to initialize all of the vector components with</param>
        /// <returns>A new vector</returns>
        public static Vector3Int ConstructVector3(int value) => new Vector3Int(value, value, value);

        /// <summary>
        /// A non native constructor for vectors
        /// </summary>
        /// <param name="value">The value to initialize all of the vector components with</param>
        /// <returns>A new vector</returns>
        public static Vector4 ConstructVector4(float value) =>
            new Vector4(value, value, value, value);
    }
}
