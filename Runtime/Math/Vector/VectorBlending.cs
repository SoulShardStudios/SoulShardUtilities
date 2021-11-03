using UnityEngine;
namespace SoulShard.Utils
{
    public partial struct VectorMath
    {
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
    }
}