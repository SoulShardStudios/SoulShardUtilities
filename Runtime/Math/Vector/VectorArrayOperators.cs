using UnityEngine;
using System.Collections.Generic;
namespace SoulShard.Utils
{
    public partial struct VectorMath
    {
        #region TranslateArray
        /// <summary>
        /// translates every vector in an array by the given vector
        /// </summary>
        /// <param name="toBeTranslated">the array to be translated</param>
        /// <param name="translateBy">the value to translate by</param>
        /// <returns>the translated array</returns>
        public static Vector2Int[] TranslateVectorArray(Vector2Int[] toBeTranslated, Vector2Int translateBy)
        {
            Vector2Int[] @return = new Vector2Int[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                @return[i] = toBeTranslated[i] + translateBy;
            return @return;
        }
        /// <summary>
        /// translates every vector in an array by the given vector
        /// </summary>
        /// <param name="toBeTranslated">the array to be translated</param>
        /// <param name="translateBy">the value to translate by</param>
        /// <returns>the translated array</returns>
        public static Vector3Int[] TranslateVectorArray(Vector3Int[] toBeTranslated, Vector3Int translateBy)
        {
            Vector3Int[] @return = new Vector3Int[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                @return[i] = toBeTranslated[i] + translateBy;
            return @return;
        }
        /// <summary>
        /// translates every vector in an array by the given vector
        /// </summary>
        /// <param name="toBeTranslated">the array to be translated</param>
        /// <param name="translateBy">the value to translate by</param>
        /// <returns>the translated array</returns>
        public static Vector2[] TranslateVectorArray(Vector2[] toBeTranslated, Vector2 translateBy)
        {
            Vector2[] @return = new Vector2[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                @return[i] = toBeTranslated[i] + translateBy;
            return @return;
        }
        /// <summary>
        /// translates every vector in an array by the given vector
        /// </summary>
        /// <param name="toBeTranslated">the array to be translated</param>
        /// <param name="translateBy">the value to translate by</param>
        /// <returns>the translated array</returns>
        public static Vector3[] TranslateVectorArray(Vector3[] toBeTranslated, Vector3 translateBy)
        {
            Vector3[] @return = new Vector3[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                @return[i] = toBeTranslated[i] + translateBy;
            return @return;
        }
        /// <summary>
        /// translates every vector in an array by the given vector
        /// </summary>
        /// <param name="toBeTranslated">the array to be translated</param>
        /// <param name="translateBy">the value to translate by</param>
        /// <returns>the translated array</returns>
        public static Vector4[] TranslateVectorArray(Vector4[] toBeTranslated, Vector4 translateBy)
        {
            Vector4[] @return = new Vector4[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                @return[i] = toBeTranslated[i] + translateBy;
            return @return;
        }
        #endregion
        #region TranslatePositionsToAllElementsInArray
        /// <summary>
        /// translates a collection of vectors to every point in the array
        /// </summary>
        /// <param name="points">the points to translate the collection to</param>
        /// <param name="shape">the collection of points to translate to every value in the array</param>
        /// <returns>the modified collection</returns>
        public static Vector2Int[] TranslateVectorsToArray(Vector2Int[] points, Vector2Int[] shape)
        {
            HashSet<Vector2Int> hash = new HashSet<Vector2Int>();
            foreach (Vector2Int v in points)
                hash.UnionWith(TranslateVectorArray(shape, v));
            Vector2Int[] @return = new Vector2Int[hash.Count];
            hash.CopyTo(@return);
            return @return;
        }
        /// <summary>
        /// translates a collection of vectors to every point in the array
        /// </summary>
        /// <param name="points">the points to translate the collection to</param>
        /// <param name="shape">the collection of points to translate to every value in the array</param>
        /// <returns>the modified collection</returns>
        public static Vector3Int[] TranslateVectorsToArray(Vector3Int[] points, Vector3Int[] shape)
        {
            HashSet<Vector3Int> hash = new HashSet<Vector3Int>();
            foreach (Vector3Int v in points)
                hash.UnionWith(TranslateVectorArray(shape, v));
            Vector3Int[] @return = new Vector3Int[hash.Count];
            hash.CopyTo(@return);
            return @return;
        }
        #endregion
        #region MultiplyArray
        /// <summary>
        /// multiplies every vector in a vector array
        /// </summary>
        /// <param name="toBeMultiplied">the array to be multiplied</param>
        /// <param name="multiplyBy">the value to be multiplied</param>
        /// <returns>the multiplied array</returns>
        public static Vector2Int[] MultiplyVectorArray(Vector2Int[] toBeMultiplied, Vector2Int multiplyBy)
        {
            Vector2Int[] @return = new Vector2Int[toBeMultiplied.Length];
            if (toBeMultiplied == null)
                return null;
            for (int i = 0; i < toBeMultiplied.Length; i++)
                @return[i] = toBeMultiplied[i] * multiplyBy;
            return @return;
        }
        /// <summary>
        /// multiplies every vector in a vector array
        /// </summary>
        /// <param name="toBeMultiplied">the array to be multiplied</param>
        /// <param name="multiplyBy">the value to be multiplied</param>
        /// <returns>the multiplied array</returns>
        public static Vector3Int[] MultiplyVectorArray(Vector3Int[] toBeMultiplied, Vector3Int multiplyBy)
        {
            Vector3Int[] @return = new Vector3Int[toBeMultiplied.Length];
            if (toBeMultiplied == null)
                return null;
            for (int i = 0; i < toBeMultiplied.Length; i++)
                @return[i] = toBeMultiplied[i] * multiplyBy;
            return @return;
        }
        /// <summary>
        /// multiplies every vector in a vector array
        /// </summary>
        /// <param name="toBeMultiplied">the array to be multiplied</param>
        /// <param name="multiplyBy">the value to be multiplied</param>
        /// <returns>the multiplied array</returns>
        public static Vector2[] MultiplyVectorArray(Vector2[] toBeMultiplied, Vector2 multiplyBy)
        {
            Vector2[] @return = new Vector2[toBeMultiplied.Length];
            if (toBeMultiplied == null)
                return null;
            for (int i = 0; i < toBeMultiplied.Length; i++)
                @return[i] = toBeMultiplied[i] * multiplyBy;
            return @return;
        }
        /// <summary>
        /// multiplies every vector in a vector array
        /// </summary>
        /// <param name="toBeMultiplied">the array to be multiplied</param>
        /// <param name="multiplyBy">the value to be multiplied</param>
        /// <returns>the multiplied array</returns>
        public static Vector3[] MultiplyVectorArray(Vector3[] toBeMultiplied, Vector3 multiplyBy)
        {
            Vector3[] @return = new Vector3[toBeMultiplied.Length];
            if (toBeMultiplied == null)
                return null;
            for (int i = 0; i < toBeMultiplied.Length; i++)
                @return[i] = MultiplyVector(toBeMultiplied[i], multiplyBy);
            return @return;
        }
        /// <summary>
        /// multiplies every vector in a vector array
        /// </summary>
        /// <param name="toBeMultiplied">the array to be multiplied</param>
        /// <param name="multiplyBy">the value to be multiplied</param>
        /// <returns>the multiplied array</returns>
        public static Vector4[] MultiplyVectorArray(Vector4[] toBeMultiplied, Vector4 multiplyBy)
        {
            Vector4[] @return = new Vector4[toBeMultiplied.Length];
            if (toBeMultiplied == null)
                return null;
            for (int i = 0; i < toBeMultiplied.Length; i++)
                @return[i] = MultiplyVector(toBeMultiplied[i], multiplyBy);
            return @return;
        }
        #endregion
        #region DivideVectorArray
        /// <summary>
        /// divides every vector in a vector array
        /// </summary>
        /// <param name="toBeDivided">the array to be divided</param>
        /// <param name="divideBy">the value to be divided</param>
        /// <returns>the divided array</returns>
        public static Vector2Int[] DivideVectorArray(Vector2Int[] toBeDivided, Vector2Int divideBy)
        {
            Vector2Int[] @return = new Vector2Int[toBeDivided.Length];
            if (toBeDivided == null)
                return null;
            for (int i = 0; i < toBeDivided.Length; i++)
                @return[i] = DivideVector(toBeDivided[i], divideBy);
            return @return;
        }
        /// <summary>
        /// divides every vector in a vector array
        /// </summary>
        /// <param name="toBeDivided">the array to be divided</param>
        /// <param name="divideBy">the value to be divided</param>
        /// <returns>the divided array</returns>
        public static Vector3Int[] DivideVectorArray(Vector3Int[] toBeDivided, Vector3Int divideBy)
        {
            Vector3Int[] @return = new Vector3Int[toBeDivided.Length];
            if (toBeDivided == null)
                return null;
            for (int i = 0; i < toBeDivided.Length; i++)
                @return[i] = DivideVector(toBeDivided[i], divideBy);
            return @return;
        }
        /// <summary>
        /// divides every vector in a vector array
        /// </summary>
        /// <param name="toBeDivided">the array to be divided</param>
        /// <param name="divideBy">the value to be divided</param>
        /// <returns>the divided array</returns>
        public static Vector2[] DivideVectorArray(Vector2[] toBeDivided, Vector2 divideBy)
        {
            Vector2[] @return = new Vector2[toBeDivided.Length];
            if (toBeDivided == null)
                return null;
            for (int i = 0; i < toBeDivided.Length; i++)
                @return[i] = toBeDivided[i] / divideBy;
            return @return;
        }
        /// <summary>
        /// divides every vector in a vector array
        /// </summary>
        /// <param name="toBeDivided">the array to be divided</param>
        /// <param name="divideBy">the value to be divided</param>
        /// <returns>the divided array</returns>
        public static Vector3[] DivideVectorArray(Vector3[] toBeDivided, Vector3 divideBy)
        {
            Vector3[] @return = new Vector3[toBeDivided.Length];
            if (toBeDivided == null)
                return null;
            for (int i = 0; i < toBeDivided.Length; i++)
                @return[i] = DivideVector(toBeDivided[i], divideBy);
            return @return;
        }
        /// <summary>
        /// divides every vector in a vector array
        /// </summary>
        /// <param name="toBeDivided">the array to be divided</param>
        /// <param name="divideBy">the value to be divided</param>
        /// <returns>the divided array</returns>
        public static Vector4[] DivideVectorArray(Vector4[] toBeDivided, Vector4 divideBy)
        {
            Vector4[] @return = new Vector4[toBeDivided.Length];
            if (toBeDivided == null)
                return null;
            for (int i = 0; i < toBeDivided.Length; i++)
                @return[i] = DivideVector(toBeDivided[i], divideBy);
            return @return;
        }
        #endregion
    }
}