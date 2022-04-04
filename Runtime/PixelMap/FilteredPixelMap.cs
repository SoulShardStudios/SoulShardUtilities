using UnityEngine;
using System;
using SoulShard.Utils;
namespace SoulShard.PixelMaps
{
    /// <summary>
    /// A pixelmap that allows for some conditional filtering.
    /// This way not all pixels are allowed to be set, 
    /// and you can have some control over wha the user is able to change.
    /// </summary>
    public class FilteredPixelMap : PixelMap
    {
        /// <summary>
        /// Function reference that filters the input to the pixel map.
        /// </summary>
        public Func<Vector2Int[], Color[], (Vector2Int[], Color[])> filter;
        // these are the same as the base class, just with some preprocessing based on filter.
        public override void SetPixel(Color color, Vector2Int position, bool apply = true)
        {
            (Vector2Int[], Color[]) pruned = filter(new Vector2Int[] { position }, new Color[] { color });
            if (pruned.Item1.Length == 0)
                return;
            base.SetPixel(color, position, apply);
        }
        public override void SetPixels(Color[] colors, Vector2Int[] positions)
        {
            (Vector2Int[], Color[]) pruned = filter(positions,colors);
            base.SetPixels(pruned.Item2, pruned.Item1);
        }
        public override void SetPixels(Color color, Vector2Int[] positions)
        {
            var pruned = filter(positions, CollectionUtility.GenerateNewArray(positions.Length, color));
            base.SetPixels(pruned.Item2, pruned.Item1);
        }
    }
}