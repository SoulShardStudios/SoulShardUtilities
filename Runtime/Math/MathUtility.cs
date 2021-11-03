using UnityEngine;
namespace SoulShard.Utils
{
    public class MathUtility : MonoBehaviour
    {
        public static uint PositiveMod(int value, uint m)
        {
            int mod = value % (int)m;
            if (mod < 0)
                mod += (int)m;
            return (uint)mod;
        }
        public static int PositiveMod(int value, int m)
        {
            int mod = value % m;
            if (mod < 0)
                mod += m;
            return mod;
        }
        public static float PositiveMod(float value, float m)
        {
            float mod = value % m;
            if (mod < 0)
                mod += m;
            return mod;
        }
    }
}
