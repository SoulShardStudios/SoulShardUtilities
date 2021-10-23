using UnityEngine;
namespace SoulShard.Utils
{
    public static class TextureHelp
    {
        public static void ConvertTexture2DFormat(ref Texture2D tex, TextureFormat format, bool mipChain) => tex = ConvertTexture2DFormat(tex, format, mipChain);
        public static Texture2D ConvertTexture2DFormat(Texture2D tex, TextureFormat format, bool mipChain)
        {
            //Create new empty Texture
            Texture2D newTex = new Texture2D(tex.width, tex.height, format, mipChain);
            //Copy old texture pixels into new one
            newTex.SetPixels(tex.GetPixels());
            //Apply
            newTex.Apply();
            return newTex;
        }
        // generates a completely empty texture to be copied to new chunks
        public static Texture2D GenerateEmptyTexture(int size, Color color)
        {
            Texture2D emptyTexture = new Texture2D(size, size);
            emptyTexture.SetPixels(0, 0, size, size,
                Methods.GenerateNewArray(size * size, color));
            emptyTexture.Apply();
            return emptyTexture;
        }
    }
}