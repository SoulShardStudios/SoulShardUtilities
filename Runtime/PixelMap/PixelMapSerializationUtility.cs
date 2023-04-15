using UnityEngine;
using System.Collections.Generic;
using SoulShard.Math;
using System.IO;
using SoulShard.Utils;

namespace SoulShard.PixelMaps
{
    public static class PixelMapSerializationUtility
    {
        /// <summary>
        /// Dumps all of the pixel map data into a folder.
        /// </summary>
        /// <param name="path">The path to dump serialize</param>
        /// <param name="map">The pixel map to serialize</param>
        public static void SerializePixelMapData(string path, PixelMap map)
        {
            if (path == null)
                return;

            Directory.CreateDirectory(path);
            var rootDir = new DirectoryInfo(path);
            foreach (FileInfo file in rootDir.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in rootDir.GetDirectories())
                dir.Delete(true);
            // Encodes all chunks as png and makes the files.
            foreach (KeyValuePair<Vector2Int, PixelChunk> k in map.chunkmap.chunks)
            {
                byte[] pngBytes = k.Value.texture.EncodeToPNG();
                string filename = k.Value.gameObject.name + ".png";
                using (FileStream stream = File.Open(path + filename, FileMode.Create))
                    stream.Write(pngBytes, 0, pngBytes.Length);
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
            string[] items = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            if (items == null)
                return null;

            // Filters contents for images.
            List<string> images = new List<string>(0);
            for (int i = 0; i < items.Length; i++)
                if (Path.GetExtension(items[i]) == ".png")
                    images.Add(items[i]);
            if (images.Count == 0)
                return null;

            // Maps the chunks to their positions in world space based off their file name
            Dictionary<Vector2Int, Texture2D> chunkPositionToTexture =
                new Dictionary<Vector2Int, Texture2D>();
            for (int i = 0; i < images.Count; i++)
            {
                byte[] fileData = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
                chunkPositionToTexture.Add(
                    VectorParser.ParseVector2IntFromString(images[i]),
                    TextureUtility.ConvertTexture2DFormat(tex, TextureFormat.RGBA32, false)
                );
            }

            return chunkPositionToTexture;
        }

        /// <summary>
        /// For the given pixel map, take the pixel map data at path, and apply it to the pixelmap.
        /// </summary>
        /// <param name="path">The location of the path of the serialized pixelmap.</param>
        /// <param name="map">The map to apply this data to.</param>
        public static void LoadPixelMapData(string path, PixelMap map) =>
            map.ApplyData(DeserializePixelMapData(path));
    }
}
