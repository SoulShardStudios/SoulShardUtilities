using UnityEngine;
namespace SoulShard.Utils
{
    public struct ColorOperators
    {
        /// <summary>
        /// applies the == operator to each element of the color
        /// </summary>
        /// <param name="original">the original color</param>
        /// <param name="other">the color to compare to</param>
        /// <returns>whether the original value is equal to the other value</returns>
        public static bool ColorEQ(Color original, Color other)
        {
            bool r = original.r == other.r;
            bool g = original.g == other.g;
            bool b = original.b == other.b;
            bool a = original.a == other.a;
            return r && g && b && a;
        }
        /// <summary>
        /// applies the == operator to each element of the color
        /// </summary>
        /// <param name="original">the original color</param>
        /// <param name="other">the color to compare to</param>
        /// <returns>whether the original value is equal to the other value</returns>
        public static bool ColorEQ(Color32 original, Color32 other)
        {
            bool r = original.r == other.r;
            bool g = original.g == other.g;
            bool b = original.b == other.b;
            bool a = original.a == other.a;
            return r && g && b && a;
        }
    }
}
