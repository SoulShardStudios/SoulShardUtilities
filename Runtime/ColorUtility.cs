using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// stores various color modification functions
    /// </summary>
    public struct _ColorUtility
    {
        /// <summary>
        /// converts a color array to a color32 array
        /// </summary>
        /// <param name="colors">the array of color32s to convert to</param>
        /// <param name="oldColors">the array of colors to convert from</param>
        public static void ConvertColorArrToColor32Arr(ref Color32[] colors, Color[] oldColors) =>
            colors = ConvertColorArrToColor32Arr(oldColors);
        /// <summary>
        /// converts a color array to a color32 array
        /// </summary>
        /// <param name="colors">the collection of colors to convert from</param>
        /// <returns>the collection of converted color32s</returns>
        public static Color32[] ConvertColorArrToColor32Arr(Color[] colors)
        {
            Color32[] @return = new Color32[colors.Length];
            for (int i = 0; i < @return.Length; i++)
                @return[i] = colors[i];
            return @return;
        }
        /// <summary>
        /// converts a color32 array to a color array
        /// </summary>
        /// <param name="colors">the array of colors to convert to</param>
        /// <param name="oldColors">he array of color32s to convert from</param>
        public static void ConvertColor32ArrToColorArr(ref Color[] colors, Color32[] oldColors) => 
            colors = ConvertColor32ArrToColorArr(oldColors);
        /// <summary>
        /// converts a color32 array to a color array
        /// </summary>
        /// <param name="colors">the collection of color32s to convert from</param>
        /// <returns>the collection of converted colors</returns>
        public static Color[] ConvertColor32ArrToColorArr(Color32[] colors)
        {
            Color[] @return = new Color[colors.Length];
            for (int i = 0; i < @return.Length; i++)
                @return[i] = colors[i];
            return @return;
        }
    }
}