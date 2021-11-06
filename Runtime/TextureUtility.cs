using UnityEngine;
namespace SoulShard.Utils
{
    public static class TextureUtility
    {
        public static void ConvertTexture2DFormat(ref Texture2D tex, TextureFormat format, bool mipChain) => tex = ConvertTexture2DFormat(tex, format, mipChain);
        public static Texture2D ConvertTexture2DFormat(Texture2D tex, TextureFormat format, bool mipChain)
        {
            //Create new empty Texture
            Texture2D @return = new Texture2D(tex.width, tex.height, format, mipChain);
            //Copy old texture pixels into new one
            @return.SetPixels(tex.GetPixels());
            //Apply
            @return.Apply();
            return @return;
        }
        // generates a completely empty texture according to the color specified
        public static Texture2D GenerateEmptyTexture(int size, Color color, TextureFormat format = TextureFormat.RGBA32, bool mipChain = true)
        {
            Texture2D @return = new Texture2D(size, size, format, mipChain);
            @return.SetPixels(0, 0, size, size,
                CollectionUtility.GenerateNewArray(size * size, color));
            @return.Apply();
            return @return;
        }
        // gets the proper texture2d from a spritesheet or regular sprite
        public static Texture2D GetTextureFromSprite(Sprite Sprite)
        {
            if (Sprite != null)
            {
                var @return = new Texture2D((int)Sprite.textureRect.width, (int)Sprite.textureRect.height);
                var pixels = Sprite.texture.GetPixels((int)Sprite.textureRect.x, (int)Sprite.textureRect.y, (int)Sprite.textureRect.width, (int)Sprite.textureRect.height);
                @return.SetPixels(pixels);
                @return.Apply();
                @return.name = Sprite.name;
                return @return;
            }
            return null;
        }
    }
}