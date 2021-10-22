using UnityEngine;
namespace SoulShard.Utils
{
    public static class TextureConvert
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
    }
}