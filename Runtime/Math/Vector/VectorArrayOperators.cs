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
    }
}