using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// Provides Multiple basic math functions specifically for vectors.
    /// translates some UnityEngine.mathf functions unto vector compatible versions
    /// </summary>
    public partial struct VectorMath
    {
        #region Clamp
        #region Floats
        /// <summary>
        /// applys mathf.Clamp() to all components of the desired vector 
        /// </summary>
        /// <param name="input"> the input vector for the operation</param>
        /// <param name="min">the minimum value allowed for all components of the vector</param>
        /// <param name="max">the maximum value allowed for all components of the vector</param>
        /// <returns></returns>
        public static Vector2 ClampVector(Vector2 @input, Vector2 min, Vector2 max)
        {
            float x = Mathf.Clamp(@input.x, min.x, max.x);
            float y = Mathf.Clamp(@input.y, min.y, max.y);
            return new Vector2(x, y);
        }
        /// <summary>
        /// applys mathf.Clamp() to all components of the desired vector 
        /// </summary>
        /// <param name="input"> the input vector for the operation</param>
        /// <param name="min">the minimum value allowed for all components of the vector</param>
        /// <param name="max">the maximum value allowed for all components of the vector</param>
        /// <returns></returns>
        public static Vector3 ClampVector(Vector3 @input, Vector3 min, Vector3 max)
        {
            float x = Mathf.Clamp(@input.x, min.x, max.x);
            float y = Mathf.Clamp(@input.y, min.y, max.y);
            float z = Mathf.Clamp(@input.z, min.z, max.z);
            return new Vector3(x, y, z);
        }
        /// <summary>
        /// applys mathf.Clamp() to all components of the desired vector 
        /// </summary>
        /// <param name="input"> the input vector for the operation</param>
        /// <param name="min">the minimum value allowed for all components of the vector</param>
        /// <param name="max">the maximum value allowed for all components of the vector</param>
        /// <returns></returns>
        public static Vector4 ClampVector(Vector4 @input, Vector4 min, Vector4 max)
        {
            float x = Mathf.Clamp(@input.x, min.x, max.x);
            float y = Mathf.Clamp(@input.y, min.y, max.y);
            float z = Mathf.Clamp(@input.z, min.z, max.z);
            float w = Mathf.Clamp(@input.w, min.w, max.w);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Ints
        /// <summary>
        /// applys mathf.Clamp() to all components of the desired vector 
        /// </summary>
        /// <param name="input"> the input vector for the operation</param>
        /// <param name="min">the minimum value allowed for all components of the vector</param>
        /// <param name="max">the maximum value allowed for all components of the vector</param>
        /// <returns></returns>
        public static Vector2Int ClampVector(Vector2Int @input, Vector2Int min, Vector2Int max)
        {
            int x = Mathf.Clamp(@input.x, min.x, max.x);
            int y = Mathf.Clamp(@input.y, min.y, max.y);
            return new Vector2Int(x, y);
        }
        /// <summary>
        /// applys mathf.Clamp() to all components of the desired vector 
        /// </summary>
        /// <param name="input"> the input vector for the operation</param>
        /// <param name="min">the minimum value allowed for all components of the vector</param>
        /// <param name="max">the maximum value allowed for all components of the vector</param>
        /// <returns></returns>
        public static Vector3Int ClampVector(Vector3Int @input, Vector3Int min, Vector3Int max)
        {
            int x = Mathf.Clamp(@input.x, min.x, max.x);
            int y = Mathf.Clamp(@input.y, min.y, max.y);
            int z = Mathf.Clamp(@input.z, min.z, max.z);
            return new Vector3Int(x, y, z);
        }
        #endregion
        #endregion
        #region Round
        /// <summary>
        /// applies Mathf.Round() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector2Int RoundVector(Vector2 @input)
        {
            int x = Mathf.RoundToInt(@input.x);
            int y = Mathf.RoundToInt(@input.y);
            return new Vector2Int(x, y);
        }
        /// <summary>
        /// applies Mathf.Round() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector3Int RoundVector(Vector3 @input)
        {
            int x = Mathf.RoundToInt(@input.x);
            int y = Mathf.RoundToInt(@input.y);
            int z = Mathf.RoundToInt(@input.z);
            return new Vector3Int(x, y, z);
        }
        /// <summary>
        /// applies Mathf.Round() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector4 RoundVector(Vector4 @input)
        {
            float x = Mathf.Round(@input.x);
            float y = Mathf.Round(@input.y);
            float z = Mathf.Round(@input.z);
            float w = Mathf.Round(@input.w);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Ceil
        /// <summary>
        /// applies Mathf.Ceil() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector2Int CeilVector(Vector2 @input)
        {
            int x = Mathf.CeilToInt(@input.x);
            int y = Mathf.CeilToInt(@input.y);
            return new Vector2Int(x, y);
        }
        /// <summary>
        /// applies Mathf.Ceil() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector3Int CeilVector(Vector3 @input)
        {
            int x = Mathf.CeilToInt(@input.x);
            int y = Mathf.CeilToInt(@input.y);
            int z = Mathf.CeilToInt(@input.z);
            return new Vector3Int(x, y, z);
        }
        /// <summary>
        /// applies Mathf.Ceil() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector4 CeilVector(Vector4 @input)
        {
            float x = Mathf.Ceil(@input.x);
            float y = Mathf.Ceil(@input.y);
            float z = Mathf.Ceil(@input.z);
            float w = Mathf.Ceil(@input.w);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Floor
        /// <summary>
        /// applies Mathf.Floor() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector2Int FloorVector(Vector2 @input)
        {
            int x = Mathf.FloorToInt(@input.x);
            int y = Mathf.FloorToInt(@input.y);
            return new Vector2Int(x, y);
        }
        /// <summary>
        /// applies Mathf.Floor() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector3Int FloorVector(Vector3 @input)
        {
            int x = Mathf.FloorToInt(@input.x);
            int y = Mathf.FloorToInt(@input.y);
            int z = Mathf.FloorToInt(@input.z);
            return new Vector3Int(x, y, z);
        }
        /// <summary>
        /// applies Mathf.Floor() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector4 FloorVector(Vector4 @input)
        {
            float x = Mathf.Floor(@input.x);
            float y = Mathf.Floor(@input.y);
            float z = Mathf.Floor(@input.z);
            float w = Mathf.Floor(@input.w);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Absolute Value
        #region Floats
        /// <summary>
        /// applies Mathf.Abs() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector2 AbsVector(Vector2 @input)
        {
            float x = Mathf.Abs(@input.x);
            float y = Mathf.Abs(@input.y);
            return new Vector2(x, y);
        }
        /// <summary>
        /// applies Mathf.Abs() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector3 AbsVector(Vector3 @input)
        {
            float x = Mathf.Abs(@input.x);
            float y = Mathf.Abs(@input.y);
            float z = Mathf.Abs(@input.z);
            return new Vector3(x, y, z);
        }
        /// <summary>
        /// applies Mathf.Abs() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector4 AbsVector(Vector4 @input)
        {
            float x = Mathf.Abs(@input.x);
            float y = Mathf.Abs(@input.y);
            float z = Mathf.Abs(@input.z);
            float w = Mathf.Abs(@input.w);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Ints
        /// <summary>
        /// applies Mathf.Abs() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector2Int AbsVector(Vector2Int @input)
        {
            int x = Mathf.Abs(@input.x);
            int y = Mathf.Abs(@input.y);
            return new Vector2Int(x, y);
        }
                /// <summary>
        /// applies Mathf.Abs() to all components of the desired vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <returns></returns>
        public static Vector3Int AbsVector(Vector3Int @input)
        {
            int x = Mathf.Abs(@input.x);
            int y = Mathf.Abs(@input.y);
            int z = Mathf.Abs(@input.z);
            return new Vector3Int(x, y, z);
        }
        #endregion
        #endregion
        #region Modulo
        #region Floats
        /// <summary>
        /// applies the modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector2 ModVector(Vector2 @input, int modby)
        {
            float x = @input.x % modby;
            float y = @input.y % modby;
            return new Vector2(x, y);
        }
        /// <summary>
        /// applies the modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector3 ModVector(Vector3 @input, int modby)
        {
            float x = @input.x % modby;
            float y = @input.y % modby;
            float z = @input.z % modby;
            return new Vector3(x, y, z);
        }
        /// <summary>
        /// applies the modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector4 ModVector(Vector4 @input, int modby)
        {
            float x = @input.x % modby;
            float y = @input.y % modby;
            float z = @input.z % modby;
            float w = @input.w % modby;
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Ints
        /// <summary>
        /// applies the modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector2Int ModVector(Vector2Int @input, int modby)
        {
            int x = @input.x % modby;
            int y = @input.y % modby;
            return new Vector2Int(x, y);
        }
                /// <summary>
        /// applies the modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector3Int ModVector(Vector3Int @input, int modby)
        {
            int x = @input.x % modby;
            int y = @input.y % modby;
            int z = @input.z % modby;
            return new Vector3Int(x, y, z);
        }
        #endregion
        #endregion
        #region PositiveModulo
        #region Floats
        /// <summary>
        /// applies the positive modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector2 PositiveModVector(Vector2 @input, int modby)
        {
            float x = MathUtility.PositiveMod(@input.x, modby);
            float y = MathUtility.PositiveMod(@input.y, modby);
            return new Vector2(x, y);
        }
        /// <summary>
        /// applies the positive modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector3 PositiveModVector(Vector3 @input, int modby)
        {
            float x = MathUtility.PositiveMod(@input.x, modby);
            float y = MathUtility.PositiveMod(@input.y, modby);
            float z = MathUtility.PositiveMod(@input.z, modby);
            return new Vector3(x, y, z);
        }
        /// <summary>
        /// applies the positive modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector4 PositiveModVector(Vector4 @input, int modby)
        {
            float x = MathUtility.PositiveMod(@input.x, modby);
            float y = MathUtility.PositiveMod(@input.y, modby);
            float z = MathUtility.PositiveMod(@input.z, modby);
            float w = MathUtility.PositiveMod(@input.w, modby);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Ints
        /// <summary>
        /// applies the positive modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector2Int PositiveModVector(Vector2Int @input, int modby)
        {
            int x = MathUtility.PositiveMod(@input.x, modby);
            int y = MathUtility.PositiveMod(@input.y, modby);
            return new Vector2Int(x, y);
        }
        /// <summary>
        /// applies the positive modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector3Int PositiveModVector(Vector3Int @input, int modby)
        {
            int x = MathUtility.PositiveMod(@input.x, modby);
            int y = MathUtility.PositiveMod(@input.y, modby);
            int z = MathUtility.PositiveMod(@input.z, modby);
            return new Vector3Int(x, y, z);
        }
        #endregion
        #endregion
        /// <summary>
        /// creates a boundsint out of a start and end coordinate
        /// </summary>
        /// <param name="start">the starting vector</param>
        /// <param name="end">the ending vector</param>
        /// <returns></returns>
        public static BoundsInt CreateBoundsInt(Vector3Int start, Vector3Int end)
        {
            Vector3Int realStart = new Vector3Int(start.x < end.x ? start.x : end.x, start.y < end.y ? start.y : end.y, 0);
            Vector3Int realEnd = new Vector3Int(start.x > end.x ? start.x : end.x, start.y > end.y ? start.y : end.y, 0);
            Vector3Int size = realEnd - realStart + new Vector3Int(0, 0, 1);
            return new BoundsInt(realStart, size);
        }
    }
}