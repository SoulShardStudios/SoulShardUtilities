using UnityEngine;
namespace SoulShard.Utils
{
    public static class VectorMath
    {
        #region VectorOperations
        #region Clamp
        public static Vector2 ClampVector2(Vector2 toClamp, Vector2 min, Vector2 max)
        {
            float x = Mathf.Clamp(toClamp.x, min.x, max.x);
            float y = Mathf.Clamp(toClamp.y, min.y, max.y);
            return new Vector2(x, y);
        }
        public static Vector3 ClampVector3(Vector3 toClamp, Vector3 min, Vector3 max)
        {
            float x = Mathf.Clamp(toClamp.x, min.x, max.x);
            float y = Mathf.Clamp(toClamp.y, min.y, max.y);
            float z = Mathf.Clamp(toClamp.z, min.z, max.z);
            return new Vector3(x, y, z);
        }
        public static Vector2Int ClampVector2Int(Vector2Int toClamp, Vector2Int min, Vector2Int max)
        {
            int x = Mathf.Clamp(toClamp.x, min.x, max.x);
            int y = Mathf.Clamp(toClamp.y, min.y, max.y);
            return new Vector2Int(x, y);
        }
        public static Vector3Int ClampVector3Int(Vector3Int toClamp, Vector3Int min, Vector3Int max)
        {
            int x = Mathf.Clamp(toClamp.x, min.x, max.x);
            int y = Mathf.Clamp(toClamp.y, min.y, max.y);
            int z = Mathf.Clamp(toClamp.z, min.z, max.z);
            return new Vector3Int(x, y, z);
        }
        #endregion
        #region Round
        public static Vector2Int RoundVector2(Vector2 toRound)
        {
            int x = Mathf.RoundToInt(toRound.x);
            int y = Mathf.RoundToInt(toRound.y);
            return new Vector2Int(x, y);
        }
        public static Vector3Int RoundVector3(Vector3 toRound)
        {
            int x = Mathf.RoundToInt(toRound.x);
            int y = Mathf.RoundToInt(toRound.y);
            int z = Mathf.RoundToInt(toRound.z);
            return new Vector3Int(x, y, z);
        }
        #endregion
        #region Ceil
        public static Vector2Int CeilVector2(Vector2 toCiel)
        {
            int x = Mathf.CeilToInt(toCiel.x);
            int y = Mathf.CeilToInt(toCiel.y);
            return new Vector2Int(x, y);
        }
        #endregion
        #region Absolute Value
        public static Vector2 AbsVector2(Vector2 toAbs)
        {
            float x = Mathf.Abs(toAbs.x);
            float y = Mathf.Abs(toAbs.y);
            return new Vector2(x, y);
        }
        public static Vector2Int AbsVector2(Vector2Int toAbs)
        {
            int x = Mathf.Abs(toAbs.x);
            int y = Mathf.Abs(toAbs.y);
            return new Vector2Int(x, y);
        }
        #endregion
        #region Modulo
        public static Vector2 ModVector2(Vector2 toMod, int modBy)
        {
            float x = toMod.x % modBy;
            float y = toMod.y % modBy;
            return new Vector2(x, y);
        }
        public static Vector2Int ModVector2(Vector2Int toMod, int modBy)
        {
            int x = toMod.x % modBy;
            int y = toMod.y % modBy;
            return new Vector2Int(x, y);
        }
        #endregion
        #endregion
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
        #endregion
        #region Blending
        public static void BlendVector2(Vector2[] blendTo, ref Vector2 toBlend) => toBlend = BlendVector2(blendTo, toBlend);
        public static Vector2 BlendVector2(Vector2[] blendTo, Vector2 toBlend)
        {
            float SmallestDist = 10;
            Vector2 StoredDir = new Vector2(0, 0);
            foreach (Vector2 V in blendTo)
            {
                float dist = Vector2.Distance(toBlend, V);
                if (SmallestDist > dist)
                {
                    SmallestDist = dist;
                    StoredDir = V;
                }
            }
            toBlend = StoredDir;
            return toBlend;
        }
        #endregion
    }
}