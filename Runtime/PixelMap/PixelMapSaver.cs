using UnityEngine;
using SoulShard.FileSystem;
using SoulShard.Utils;

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
        public void SaveData() => PixelMapSerializationUtility.SerializeData(GetPath(), _map);

        /// <summary>
        /// Load the pixel maps data.
        /// </summary>
        public void LoadData()
        {
            _map.HardReset();
            PixelMapSerializationUtility.LoadPixelMapData(GetPath(), _map);
        }
    }
}
