using UnityEngine;
using System.Collections.Generic;
namespace SoulShard.Utils
{
    public partial struct VectorMath
    {
        #region TranslateArray
        // translates every vector in an array by the given vector
        public static Vector2Int[] TranslateVectorArray(Vector2Int[] toBeTranslated, Vector2Int translateBy)
        {
            Vector2Int[] toReturn = new Vector2Int[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                toReturn[i] = toBeTranslated[i] + translateBy;
            return toReturn;
        }
        public static Vector3Int[] TranslateVectorArray(Vector3Int[] toBeTranslated, Vector3Int translateBy)
        {
            Vector3Int[] toReturn = new Vector3Int[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                toReturn[i] = toBeTranslated[i] + translateBy;
            return toReturn;
        }
        public static Vector2[] TranslateVectorArray(Vector2[] toBeTranslated, Vector2 translateBy)
        {
            Vector2[] toReturn = new Vector2[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                toReturn[i] = toBeTranslated[i] + translateBy;
            return toReturn;
        }
        public static Vector3[] TranslateVectorArray(Vector3[] toBeTranslated, Vector3 translateBy)
        {
            Vector3[] toReturn = new Vector3[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                toReturn[i] = toBeTranslated[i] + translateBy;
            return toReturn;
        }
        public static Vector4[] TranslateVectorArray(Vector4[] toBeTranslated, Vector4 translateBy)
        {
            Vector4[] toReturn = new Vector4[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                toReturn[i] = toBeTranslated[i] + translateBy;
            return toReturn;
        }
        #endregion
        #region TranslatePositionsToAllElementsInArray
        public static Vector2Int[] TranslateVectorsToArray(Vector2Int[] points, Vector2Int[] shape)
        {
            HashSet<Vector2Int> hash = new HashSet<Vector2Int>();
            foreach (Vector2Int v in points)
                hash.UnionWith(TranslateVectorArray(shape, v));
            Vector2Int[] toReturn = new Vector2Int[hash.Count];
            hash.CopyTo(toReturn);
            return toReturn;
        }
        public static Vector3Int[] TranslateVectorsToArray(Vector3Int[] points, Vector3Int[] shape)
        {
            HashSet<Vector3Int> hash = new HashSet<Vector3Int>();
            foreach (Vector3Int v in points)
                hash.UnionWith(TranslateVectorArray(shape, v));
            Vector3Int[] toReturn = new Vector3Int[hash.Count];
            hash.CopyTo(toReturn);
            return toReturn;
        }
        #endregion
    }
}