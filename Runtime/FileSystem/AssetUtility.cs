using UnityEngine;
using SoulShard.Utils;
using System.IO;
namespace SoulShard.FileSystem
{
    /// <summary>
    /// contains multiple functions related to managing game assets and their files
    /// </summary>
    public static class AssetUtility
    {
        /// <summary>
        /// loads a Texture2D from a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <param name="format">the format to set to the imported texture</param>
        /// <param name="mipChain">the mipchain value to set to the imported texture</param>
        /// <returns>the imported Texture2D</returns>
        public static Texture2D LoadTexture2D(string path, TextureFormat format, bool mipChain) => LoadTexture2DRawPath(PathUtility.ParsePath(path), format, mipChain);
        /// <summary>
        /// loads a Texture2D from a given raw path with no path preprocessing
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <param name="format">the format to set to the imported texture</param>
        /// <param name="mipChain">the mipchain value to set to the imported texture</param>
        /// <returns>the imported Texture2D</returns>
        public static Texture2D LoadTexture2DRawPath(string path, TextureFormat format, bool mipChain)
        {
            if (!File.Exists(path))
                return null;
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            TextureUtility.ConvertTexture2DFormat(ref tex, format, mipChain);
            return tex;
        }
        /// <summary>
        /// loads the text from a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the contents of the file in text form</returns>
        public static string LoadText(string path) => LoadTextRawPath(PathUtility.ParsePath(path));
        /// <summary>
        /// loads the text from a given path with no path preprocessing
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the contents of the file in text form</returns>
        public static string LoadTextRawPath(string path)
        {
            if (!File.Exists(path))
                return null;
            return File.ReadAllText(path);
        }
    }
}
