using UnityEngine;
using System.Collections.Generic;
namespace SoulShard.Utils
{
    public partial struct VectorMath
    {
        #region VectorArrayOperations
        // translates every vector in an array by the given vector
        public static Vector2Int[] TranslateVector2IntArray(Vector2Int[] toBeTranslated, Vector2Int translateBy)
        {
            Vector2Int[] toReturn = new Vector2Int[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                toReturn[i] = toBeTranslated[i] + translateBy;
            return toReturn;
        }
        public static Vector3Int[] TranslateVector3IntArray(Vector3Int[] toBeTranslated, Vector3Int translateBy)
        {
            Vector3Int[] toReturn = new Vector3Int[toBeTranslated.Length];
            if (toBeTranslated == null)
                return null;
            for (int i = 0; i < toBeTranslated.Length; i++)
                toReturn[i] = toBeTranslated[i] + translateBy;
            return toReturn;
        }
        public static Vector2Int[] TranslatePositionsToArray(Vector2Int[] points, Vector2Int[] shape)
        {
            HashSet<Vector2Int> hash = new HashSet<Vector2Int>();
            foreach (Vector2Int v in points)
                hash.UnionWith(TranslateVector2IntArray(shape, v));
            Vector2Int[] toReturn = new Vector2Int[hash.Count];
            hash.CopyTo(toReturn);
            return toReturn;
        }
        #endregion
    }
}