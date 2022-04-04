using UnityEngine;
using System.Collections.Generic;
using SoulShard.Math;
using SoulShard.FileSystem;
namespace SoulShard.PixelMaps
{
    /// <summary>
    /// Saves the contents of the associated pixelmap to file.
    /// </summary>
    [RequireComponent(typeof(PixelMap))]
    public class PixelMapSaver : SingleInitMono
    {
        /// <summary>
        /// The location to save the chunked images to.
        /// </summary>
        public string saveLocation;
        /// <summary>
        /// On object enable, initialize the map with the saved data.
        /// </summary>
        public bool onEnableLoadData;
        /// <summary>
        /// The associated pixel map that this manages save data for
        /// </summary>
        PixelMap _map;
        /// <summary>
        /// Whether this component has been initialized
        /// </summary>
        bool _initialized;
        /// <summary>
        /// Initialization for this component
        /// </summary>
        public override void Init()
        {
            if (_initialized)
                return;
            _map = GetComponent<PixelMap>();
            if (onEnableLoadData)
                LoadData();
        }
        /// <summary>
        /// Get the parsed save location.
        /// </summary>
        string GetPath()
        {
            if (saveLocation != "")
                return PathUtility.ParsePath(saveLocation) + '/';
            return null;
        }
        /// <summary>
        /// Save the pixel maps data.
        /// </summary>
        public void SaveData()
        {
            // Get Path.
            string path = GetPath();
            if (path == null)
                return;

            // make sure the directory exists
            DirectoryUtility.CreateDir(path);

            // Delete all of its contents, as we have new contents to give it.
            // Difference analysis could be done, but this was easier :)
            DirectoryUtility.DeleteAllContentsInDirectory(path);

            // Encodes all chunks as png and makes the files.
            foreach (KeyValuePair<Vector2Int, PixelChunk> k in _map.chunkmap.chunks)
            {
                byte[] pngBytes = k.Value.texture.EncodeToPNG();
                string filename = k.Value.gameObject.name + ".png";
                FileUtility.MakeFile(path + filename, pngBytes);
            }
        }
        /// <summary>
        /// Load the pixel maps data.
        /// </summary>
        public void LoadData()
        {
            // Totally resets the map.
            _map.HardReset();

            // Get path.
            string path = GetPath();
            if (path == null)
                return;

            // Get all files.
            string[] items = DirectoryUtility.GetAllFilePathsInDirectory(path);
            if (items == null)
                return;

            // Filters contents for images.
            List<string> images = new List<string>(0);
            for (int i = 0; i < items.Length; i++)
                if (FileUtility.GetFileExt(items[i]) == ".png")
                    images.Add(items[i]);
            if (images.Count == 0)
                return;

            // Maps the chunks to their positions in world space based off their file name
            Dictionary<Vector2Int, Texture2D> chunkPositionToTexture = new Dictionary<Vector2Int, Texture2D>();
            for (int i = 0; i < images.Count; i++)
                chunkPositionToTexture.Add(VectorParser.ParseVector2IntFromString(images[i]), AssetUtility.LoadTexture2D(images[i], TextureFormat.RGBA32, false));
            
            // Uses the map to initialize the chunks.
            foreach (KeyValuePair<Vector2Int, Texture2D> k in chunkPositionToTexture)
            {
                k.Value.name = k.Key.ToString();
                _map.AddChunk(k.Key, k.Value);
            }
        }
    }
}
