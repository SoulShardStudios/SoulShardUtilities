using UnityEngine;
namespace SoulShard.Utils
{
    public struct _ColorUtility
    {
        // just converts a color32 array to colors array, or vice versa
        public static void ConvertColorArrToColor32Arr(ref Color32[] colors, Color[] oldColors) =>
            colors = ConvertColorArrToColor32Arr(oldColors);
        public static Color32[] ConvertColorArrToColor32Arr(Color[] colors)
        {
            Color32[] @return = new Color32[colors.Length];
            for (int i = 0; i < @return.Length; i++)
                @return[i] = colors[i];
            return @return;
        }
        public static void ConvertColor32ArrToColorArr(ref Color[] colors, Color32[] oldColors) => 
            colors = ConvertColor32ArrToColorArr(oldColors);
        public static Color[] ConvertColor32ArrToColorArr(Color32[] colors)
        {
            Color[] @return = new Color[colors.Length];
            for (int i = 0; i < @return.Length; i++)
                @return[i] = colors[i];
            return @return;
        }
    }
}