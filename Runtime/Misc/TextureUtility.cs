using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// contains general functionality for editing textures at runtime
    /// </summary>
    public static class TextureUtility
    {
        /// <summary>
        /// converts the format of a Texture2D
        /// </summary>
        /// <param name="tex">the texture to modify the properties of</param>
        /// <param name="format">the new format for the texture</param>
        /// <param name="mipChain">the new mipchain value for the texture</param>
        public static void ConvertTexture2DFormat(
            ref Texture2D tex,
            TextureFormat format,
            bool mipChain
        ) => tex = ConvertTexture2DFormat(tex, format, mipChain);

        /// <summary>
        /// converts the format of a Texture2D
        /// </summary>
        /// <param name="tex">the input texture to modify</param>
        /// <param name="format">the new format for the texture</param>
        /// <param name="mipChain">the new mipchain value for the texture</param>
        /// <returns>the edited texture</returns>
        public static Texture2D ConvertTexture2DFormat(
            Texture2D tex,
            TextureFormat format,
            bool mipChain
        )
        {
            //Create new empty Texture
            Texture2D @return = new Texture2D(tex.width, tex.height, format, mipChain);
            //Copy old texture pixels into new one
            @return.SetPixels(tex.GetPixels());
            //Apply
            @return.Apply();
            return @return;
        }

        /// <summary>
        /// generates a completely empty texture according to the color specified
        /// </summary>
        /// <param name="size">the size of the new texture</param>
        /// <param name="color">the color to apply to every pixel of the new texture</param>
        /// <param name="format">the format of the new texture</param>
        /// <param name="mipChain">the mipchain value of the new texture</param>
        /// <returns>the new empty texture</returns>
        public static Texture2D GenerateEmptyTexture(
            int size,
            Color color,
            TextureFormat format = TextureFormat.RGBA32,
            bool mipChain = true
        )
        {
            Texture2D @return = new Texture2D(size, size, format, mipChain);
            @return.SetPixels(
                0,
                0,
                size,
                size,
                CollectionUtility.GenerateNewArray(size * size, color)
            );
            @return.Apply();
            return @return;
        }

        /// <summary>
        /// gets the proper texture2d from a spritesheet or regular sprites
        /// </summary>
        /// <param name="Sprite">the sprite to get the texture from</param>
        /// <returns>the texture from the proper uv coordinates</returns>
        public static Texture2D GetTextureFromSprite(Sprite Sprite)
        {
            if (Sprite != null)
            {
                var @return = new Texture2D(
                    (int)Sprite.textureRect.width,
                    (int)Sprite.textureRect.height
                );
                var pixels = Sprite.texture.GetPixels(
                    (int)Sprite.textureRect.x,
                    (int)Sprite.textureRect.y,
                    (int)Sprite.textureRect.width,
                    (int)Sprite.textureRect.height
                );
                @return.SetPixels(pixels);
                @return.Apply();
                @return.name = Sprite.name;
                return @return;
            }
            return null;
        }
    }
}
