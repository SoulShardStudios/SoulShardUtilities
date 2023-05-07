using UnityEngine;

namespace SoulShard.Math
{
    public static class RectUtility
    {
        // see https://www.desmos.com/calculator/gc6sog1flh
        public static Rect ScaleRectCenterPivot(this Rect rect, float percentage)
        {
            var padding = (rect.size - (rect.size * percentage)) / 2;
            return new Rect(rect.min + padding, rect.size - padding);
        }
    }
}
