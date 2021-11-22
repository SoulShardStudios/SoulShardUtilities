namespace SoulShard.Math
{
    /// <summary>
    /// provides multiple basic math functions, not included in UnityEngine.mathf
    /// </summary>
    public partial struct MathUtility
    {
        /// <summary>
        /// function for computing positive modulus
        /// </summary>
        /// <param name="value">the value to apply this operator to</param>
        /// <param name="m">the value to modulus by</param>
        /// <returns>the modified value</returns>
        public static uint PositiveMod(int value, uint m)
        {
            int mod = value % (int)m;
            if (mod < 0)
                mod += (int)m;
            return (uint)mod;
        }
        /// <summary>
        /// function for computing positive modulus
        /// </summary>
        /// <param name="value">the value to apply this operator to</param>
        /// <param name="m">the value to modulus by</param>
        /// <returns>the modified value</returns>
        public static int PositiveMod(int value, int m)
        {
            int mod = value % m;
            if (mod < 0)
                mod += m;
            return mod;
        }
        /// <summary>
        /// function for computing positive modulus
        /// </summary>
        /// <param name="value">the value to apply this operator to</param>
        /// <param name="m">the value to modulus by</param>
        /// <returns>the modified value</returns>
        public static float PositiveMod(float value, float m)
        {
            float mod = value % m;
            if (mod < 0)
                mod += m;
            return mod;
        }
    }
}
