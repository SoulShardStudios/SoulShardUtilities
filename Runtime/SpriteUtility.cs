using UnityEngine;
namespace SoulShard.Utils
{
    public static class SpriteUtility
    {
        // gets the current image, and sets the pivot either to a specific position, or the height of the pixel closest to the bottom of the image
        public static Sprite SetPivot(Sprite S, Vector2 Pivot) { return Sprite.Create(S.texture, new Rect(0, 0, S.texture.width, S.texture.height), Pivot, 1); }
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