using UnityEngine;
using System;

namespace SoulShard.Math
{
    /// <summary>
    /// Provides Multiple basic math functions specifically for vectors.
    /// translates some UnityEngine.mathf functions unto vector compatible versions
    /// </summary>
    public partial struct VectorMath
    {
        #region Generics
        #region With Operating Number
        #region Ints
        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operateWith">the value to apply the operation with</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector2Int VectorOperation(
            Vector2Int @input,
            Vector2Int operateWith,
            Func<int, int, int> operation
        )
        {
            int x = operation(@input.x, operateWith.x);
            int y = operation(@input.y, operateWith.y);
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operateWith">the value to apply the operation with</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector3Int VectorOperation(
            Vector3Int @input,
            Vector3Int operateWith,
            Func<int, int, int> operation
        )
        {
            int x = operation(@input.x, operateWith.x);
            int y = operation(@input.y, operateWith.y);
            int z = operation(@input.z, operateWith.z);
            return new Vector3Int(x, y, z);
        }
        #endregion
        #region Floats
        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operateWith">the value to apply the operation with</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector2 VectorOperation(
            Vector2 @input,
            Vector2 operateWith,
            Func<float, float, float> operation
        )
        {
            float x = operation(@input.x, operateWith.x);
            float y = operation(@input.y, operateWith.y);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operateWith">the value to apply the operation with</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector3 VectorOperation(
            Vector3 @input,
            Vector3 operateWith,
            Func<float, float, float> operation
        )
        {
            float x = operation(@input.x, operateWith.x);
            float y = operation(@input.y, operateWith.y);
            float z = operation(@input.z, operateWith.z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operateWith">the value to apply the operation with</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector4 VectorOperation(
            Vector4 @input,
            Vector4 operateWith,
            Func<float, float, float> operation
        )
        {
            float x = operation(@input.x, operateWith.x);
            float y = operation(@input.y, operateWith.y);
            float z = operation(@input.z, operateWith.z);
            float w = operation(@input.w, operateWith.w);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #endregion
        #region Without Operating Number
        #region Ints
        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector2Int VectorOperation(Vector2Int @input, Func<int, int> operation)
        {
            int x = operation(@input.x);
            int y = operation(@input.y);
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector3Int VectorOperation(Vector3Int @input, Func<int, int> operation)
        {
            int x = operation(@input.x);
            int y = operation(@input.y);
            int z = operation(@input.z);
            return new Vector3Int(x, y, z);
        }
        #endregion
        #region Floats
        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector2 VectorOperation(Vector2 @input, Func<float, float> operation)
        {
            float x = operation(@input.x);
            float y = operation(@input.y);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector3 VectorOperation(Vector3 @input, Func<float, float> operation)
        {
            float x = operation(@input.x);
            float y = operation(@input.y);
            float z = operation(@input.z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Applies an operation to all components of a vector
        /// </summary>
        /// <param name="input">the input value to apply the operation to</param>
        /// <param name="operation">the operation to apply</param>
        /// <returns>the modified vector</returns>
        public static Vector4 VectorOperation(Vector4 @input, Func<float, float> operation)
        {
            float x = operation(@input.x);
            float y = operation(@input.y);
            float z = operation(@input.z);
            float w = operation(@input.w);
            return new Vector4(x, y, z, w);
        }
        #endregion
        #endregion
        #endregion
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
        #region Rounders
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
        public static Vector2 ModVector(Vector2 @input, float modby)
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
        public static Vector3 ModVector(Vector3 @input, float modby)
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
        public static Vector4 ModVector(Vector4 @input, float modby)
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
        #region Positive Modulo
        #region Floats
        /// <summary>
        /// applies the positive modulo operator on all components of a vector
        /// </summary>
        /// <param name="input">the input vector for the operation</param>
        /// <param name="modby">the value to modulo the vector by </param>
        /// <returns></returns>
        public static Vector2 PositiveModVector(Vector2 @input, float modby)
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
        public static Vector3 PositiveModVector(Vector3 @input, float modby)
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
        public static Vector4 PositiveModVector(Vector4 @input, float modby)
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
        #region Missing Basic Operators
        #region Multiplication
        /// <summary>
        /// unity doesn't support this basic operation on this type for some reason, this is a non-native stand in
        /// </summary>
        /// <param name="input">the value to be multiplied</param>
        /// <param name="multiplyBy">the value to multiply by</param>
        /// <returns>the multiplied value</returns>
        public static Vector3 MultiplyVector(Vector3 @input, Vector3 multiplyBy)
        {
            float x = @input.x * multiplyBy.x;
            float y = @input.y * multiplyBy.y;
            float z = @input.z * multiplyBy.z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// unity doesn't support this basic operation on this type for some reason, this is a non-native stand in
        /// </summary>
        /// <param name="input">the value to be multiplied</param>
        /// <param name="multiplyBy">the value to multiply by</param>
        /// <returns>the multiplied value</returns>
        public static Vector4 MultiplyVector(Vector4 @input, Vector4 multiplyBy)
        {
            float x = @input.x * multiplyBy.x;
            float y = @input.y * multiplyBy.y;
            float z = @input.z * multiplyBy.z;
            float w = @input.w * multiplyBy.w;
            return new Vector4(x, y, z, w);
        }
        #endregion
        #region Division
        /// <summary>
        /// unity doesn't support this basic operation on this type for some reason, this is a non-native stand in
        /// </summary>
        /// <param name="input">the value to be divided</param>
        /// <param name="divideBy">the value to divide by</param>
        /// <returns>the divided value</returns>
        public static Vector2Int DivideVector(Vector2Int @input, Vector2Int divideBy)
        {
            int x = @input.x / divideBy.x;
            int y = @input.y / divideBy.y;
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// unity doesn't support this basic operation on this type for some reason, this is a non-native stand in
        /// </summary>
        /// <param name="input">the value to be divided</param>
        /// <param name="divideBy">the value to divide by</param>
        /// <returns>the divided value</returns>
        public static Vector3Int DivideVector(Vector3Int @input, Vector3Int divideBy)
        {
            int x = @input.x / divideBy.x;
            int y = @input.y / divideBy.y;
            int z = @input.z / divideBy.z;
            return new Vector3Int(x, y, z);
        }

        /// <summary>
        /// unity doesn't support this basic operation on this type for some reason, this is a non-native stand in
        /// </summary>
        /// <param name="input">the value to be divided</param>
        /// <param name="divideBy">the value to divide by</param>
        /// <returns>the divided value</returns>
        public static Vector3 DivideVector(Vector3 @input, Vector3 divideBy)
        {
            float x = @input.x / divideBy.x;
            float y = @input.y / divideBy.y;
            float z = @input.z / divideBy.z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// unity doesn't support this basic operation on this type for some reason, this is a non-native stand in
        /// </summary>
        /// <param name="input">the value to be divided</param>
        /// <param name="divideBy">the value to divide by</param>
        /// <returns>the divided value</returns>
        public static Vector4 DivideVector(Vector4 @input, Vector4 divideBy)
        {
            float x = @input.x / divideBy.x;
            float y = @input.y / divideBy.y;
            float z = @input.z / divideBy.z;
            float w = @input.w / divideBy.w;
            return new Vector4(x, y, z, w);
        }
        #endregion
        #endregion
        #region Other
        /// <summary>
        /// creates a boundsint out of a start and end coordinate
        /// </summary>
        /// <param name="start">the starting vector</param>
        /// <param name="end">the ending vector</param>
        /// <returns></returns>
        public static BoundsInt CreateBoundsInt(Vector3Int start, Vector3Int end)
        {
            Vector3Int realStart = new Vector3Int(
                start.x < end.x ? start.x : end.x,
                start.y < end.y ? start.y : end.y,
                0
            );
            Vector3Int realEnd = new Vector3Int(
                start.x > end.x ? start.x : end.x,
                start.y > end.y ? start.y : end.y,
                0
            );
            Vector3Int size = realEnd - realStart + new Vector3Int(0, 0, 1);
            return new BoundsInt(realStart, size);
        }
        #endregion
    }
}
