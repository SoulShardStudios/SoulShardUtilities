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
                General.GenerateNewArray(size * size, color));
            @return.Apply();
            return @return;
        }
    }
}