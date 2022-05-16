using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// contains general functionality for editing sprite properties at runtime
    /// </summary>
    public static class SpriteUtility
    {
        /// <summary>
        /// gets the current sprite, and sets the pivot to a specific position
        /// </summary>
        /// <param name="S">the sprite to modify the pivot of</param>
        /// <param name="Pivot">the pivot to set the sprite to</param>
        /// <returns>the modified sprite</returns>
        public static Sprite SetPivot(Sprite S, Vector2 Pivot)
        {
            return Sprite.Create(
                S.texture,
                new Rect(0, 0, S.texture.width, S.texture.height),
                Pivot,
                1
            );
        }

        /// <summary>
        /// gets the current sprite, and sets the pivot to the height of the pixel closest to the bottom of the image
        /// </summary>
        /// <param name="S">the sprite to modify the pivot of</param>
        /// <returns>the modified sprite</returns>
        public static Sprite SetPivotBottomMost(Sprite S)
        {
            Texture2D T = S.texture;
            Color[] colors = T.GetPixels();
            float YPos = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].a != 0)
                {
                    YPos = (((float)i / (float)T.width) / (float)T.height);
                    break;
                }
            }
            return Sprite.Create(T, new Rect(0, 0, T.width, T.height), new Vector2(0.5f, YPos), 1);
        }
    }
}
