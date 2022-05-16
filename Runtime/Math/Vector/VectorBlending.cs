using UnityEngine;

namespace SoulShard.Math
{
    public partial struct VectorMath
    {
        /// <summary>
        /// blending is used for selecting the closest vector to the one to blend out of the blendTo List.
        /// this is useful when deciding what animation direction to play
        /// </summary>
        /// <param name="blendTo">The list of vectors that can be used in blending</param>
        /// <param name="toBlend">The vector to blend</param>
        public static void BlendVector2(Vector2[] blendTo, ref Vector2 toBlend) =>
            toBlend = BlendVector2(blendTo, toBlend);

        /// <summary>
        /// blending is used for selecting the closest vector to the one to blend out of the blendTo List.
        /// this is useful when deciding what animation direction to play
        /// </summary>
        /// <param name="blendTo">The list of vectors that can be used in blending</param>
        /// <param name="toBlend">The vector to blend</param>
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
