using UnityEngine;
using SoulShard.FileSystem;
using System.Collections.Generic;
using SoulShard.Math;
namespace SoulShard.PixelMaps
{
    public static class PixelMapSerializationUtility
    {
        /// <summary>
        /// Dumps all of the pixel map data into a folder.
        /// </summary>
        /// <param name="path">The path to dump serialize</param>
        /// <param name="map">The pixel map to serialize</param>
        public static void SerializeData(string path, PixelMap map)
        {
            if (path == null)
                return;

            // Make sure the directory exists.
            DirectoryUtility.Create(path);

            // Delete all of its contents, as we have new contents to give it.
            // Difference analysis could be done, but this was easier :)
            DirectoryUtility.DeleteAllContents(path);

            // Encodes all chunks as png and makes the files.
            foreach (KeyValuePair<Vector2Int, PixelChunk> k in map.chunkmap.chunks)
            {
                byte[] pngBytes = k.Value.texture.EncodeToPNG();
                string filename = k.Value.gameObject.name + ".png";
                FileUtility.Make(path + filename, pngBytes);
            }
        }

        /// <summary>
        /// Given a path to some serialized pixel map data this will fetch and deserialize all of that information.
        /// </summary>
        /// <param name="path">The path of the serialized pixel map info.</param>
        /// <returns>A dict of Vecotr2Int to Texture2D, which can be put into a pixelmap.</returns>
        public static Dictionary<Vector2Int, Texture2D> DeserializePixelMapData(string path)
        {
            if (path == null)
                return null;

            // Get all files.
            string[] items = DirectoryUtility.GetAllFilePaths(path);
            if (items == null)
                return null;

            // Filters contents for images.
            List<string> images = new List<string>(0);
            for (int i = 0; i < items.Length; i++)
                if (FileUtility.GetExt(items[i]) == ".png")
                    images.Add(items[i]);
            if (images.Count == 0)
                return null;

            // Maps the chunks to their positions in world space based off their file name
            Dictionary<Vector2Int, Texture2D> chunkPositionToTexture = new Dictionary<Vector2Int, Texture2D>();
            for (int i = 0; i < images.Count; i++)
                chunkPositionToTexture.Add(VectorParser.ParseVector2IntFromString(images[i]), AssetUtility.LoadTexture2D(images[i], TextureFormat.RGBA32, false));
            return chunkPositionToTexture;
        }

        /// <summary>
        /// For the given pixel map, take the pixel map data at path, and apply it to the pixelmap.
        /// </summary>
        /// <param name="path">The location of the path of the serialized pixelmap.</param>
        /// <param name="map">The map to apply this data to.</param>
        public static void LoadPixelMapData(string path, PixelMap map) => map.ApplyData(DeserializePixelMapData(path));
    }
}
