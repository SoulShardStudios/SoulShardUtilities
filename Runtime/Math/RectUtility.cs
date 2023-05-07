using UnityEngine;

namespace SoulShard.Math
{
    public static class RectUtility
    {
        // see https://www.desmos.com/calculator/gc6sog1flh
        public static Rect ScaleRectCenterPivot(this Rect rect, float percentage)
        {
            var scaledSize = rect.size * percentage;
            var padMinPos = rect.min + (rect.size - scaledSize) / 2;
            return new Rect(padMinPos, scaledSize);
        }
    }
}
