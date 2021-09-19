using UnityEngine;
namespace SoulShard.Utils
{
    public static class ColorEditing
    {
        #region Red
        public static Color ModRed(Color C, float amount) { return new Color(C.r + amount, C.g, C.b, C.a); }
        public static void ModRed(ref Color C, float amount) => C = new Color(C.r + amount, C.g, C.b, C.a);
        public static Color SetRed(Color C, float amount) { return new Color(amount, C.g, C.b, C.a); }
        public static void SetRed(ref Color C, float amount) => C = new Color(amount, C.g, C.b, C.a);
        #endregion
        #region Green
        public static Color ModGreen(Color C, float amount) { return new Color(C.r, C.g + amount, C.b, C.a); }
        public static void ModGreen(ref Color C, float amount) => C = new Color(C.r, C.g + amount, C.b, C.a);
        public static Color SetGreen(Color C, float amount) { return new Color(C.r, amount, C.b, C.a); }
        public static void SetGreen(ref Color C, float amount) => C = new Color(C.r, amount, C.b, C.a);
        #endregion
        #region Blue
        public static Color ModBlue(Color C, float amount) { return new Color(C.r, C.g, C.b + amount, C.a); }
        public static void ModBlue(ref Color C, float amount) => C = new Color(C.r, C.g, C.b + amount, C.a);
        public static Color SetBlue(Color C, float amount) { return new Color(C.r, C.g, amount, C.a); }
        public static void SetBlue(ref Color C, float amount) => C = new Color(C.r, C.g, amount, C.a);
        #endregion
        #region Alpha
        public static Color ModAlpha(Color C, float amount) { return new Color(C.r, C.g, C.b, C.a + amount); }
        public static void ModAlpha(ref Color C, float amount) => C = new Color(C.r, C.g, C.b, C.a + amount);
        public static Color SetAlpha(Color C, float amount) { return new Color(C.r, C.g, C.b, amount); }
        public static void SetAlpha(ref Color C, float amount) => C = new Color(C.r, C.g, C.b, amount);
        #endregion
    }
}