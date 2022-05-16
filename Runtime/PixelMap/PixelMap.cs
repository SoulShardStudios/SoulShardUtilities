using UnityEngine;
using System.Collections.Generic;
using SoulShard.Utils;
using SoulShard.Math;
using Unity.Collections;
using System;

namespace SoulShard.PixelMaps
{
    /// <summary>
    /// A class that enables pixel by pixel editing across an infinite plane.
    /// </summary>
    public partial class PixelMap : ChunkMapInt2DMono<PixelChunk>
    {
        #region Vars
        /// <summary>
        /// Whether this pixelmap should send updates via mapChangeCallback when it is updated.
        /// </summary>
        [SerializeField]
        bool shouldCallbackChanges;

        /// <summary>
        /// The callback for when this is edited.
        /// </summary>
        public Action<Vector2Int[], Color[], PixelMap> mapChangeCallback;

        /// <summary>
        /// The callback for when the whole map is cleared.
        /// </summary>
        public Action mapClearCallback;

        #region Chunk Vars
        /// <summary>
        /// The transparency of the map.
        /// </summary>
        [Header(header: "chunk size MUST be an odd number")]
        [Range(0, 1)]
        [SerializeField]
        float _transparency = 1;

        /// <summary>
        /// The sorting layer of the map.
        /// </summary>
        [SerializeField]
        string _sortLayer;

        /// <summary>
        /// The sorting order of the map.
        /// </summary>
        [SerializeField]
        int _sortOrder;

        /// <summary>
        /// The empty color of this map.
        /// </summary>
        public Color emptyPixelColor;

        /// <summary>
        /// The empty texture that gets copied to new chunks.
        /// </summary>
        Texture2D _emptyTexture;
        #endregion
        #endregion
        #region Unity Hooks
        /// <summary>
        /// Init the empty texture on enable.
        /// </summary>
        protected virtual void OnEnable() =>
            _emptyTexture = TextureUtility.GenerateEmptyTexture(
                (int)chunkmap.chunkSize,
                emptyPixelColor,
                mipChain: false
            );
        #endregion
        #region Getters And Setters
        /// <summary>
        /// Sets a pixel at a given x and y with a color.
        /// </summary>
        /// <param name="color">The color to set the pixel to.</param>
        /// <param name="position">The position of the pixel to edit.</param>
        /// <param name="apply">Whether the underlying chunk's texture changes for this pixel should be applied.</param>
        public virtual void SetPixel(Color color, Vector2Int position, bool apply = true)
        {
            ChunkPosition chunkPos = chunkmap.GetChunkPos(position);
            AddChunk(chunkPos.outer);
            PixelChunk chunk = chunkmap.GetChunk(chunkPos.outer);
            chunk?.texture.SetPixel(chunkPos.inner.x, chunkPos.inner.y, color);
            Vector2Int[] positions = new Vector2Int[1] { position };
            Color[] colors = new Color[1] { color };
            mapChangeCallback?.Invoke(positions, colors, this);
            if (apply)
                chunk?.texture.Apply();
        }

        /// <summary>
        /// Sets all given pixel positions to this color.
        /// </summary>
        /// <param name="color">The color to set all positions to.</param>
        /// <param name="positions">The pixel positions to edit.</param>
        public virtual void SetPixels(Color color, Vector2Int[] positions)
        {
            mapChangeCallback?.Invoke(
                positions,
                CollectionUtility.GenerateNewArray(positions.Length, color),
                this
            );
            Jobs.EditJobScedule<Color32, Jobs.SetPixelsJobSingleColor>(positions, color, this);
        }

        /// <summary>
        /// Sets a group of pixels.
        /// </summary>
        /// <param name="colors">The colors of these new pixels.</param>
        /// <param name="positions">The positions of these new pixels</param>
        public virtual void SetPixels(Color[] colors, Vector2Int[] positions)
        {
            mapChangeCallback?.Invoke(positions, colors, this);
            NativeArray<Color32> n_colors = new NativeArray<Color32>(
                _ColorUtility.ConvertColorArrToColor32Arr(colors),
                Allocator.TempJob
            );
            Jobs.EditJobScedule<NativeArray<Color32>, Jobs.SetPixelsJob>(positions, n_colors, this);
            n_colors.Dispose();
        }

        /// <summary>
        /// Gets the color of a given pixel.
        /// </summary>
        /// <param name="position">The position of the pixel.</param>
        /// <returns>The color of the pixel at that position</returns>
        public virtual Color GetPixel(Vector2Int position)
        {
            Vector2Int innerChunkPos = chunkmap.GetInnerChunkPos(position);
            if (chunkmap.GetChunk(chunkmap.GetOuterChunkPos(position)) == null)
                return emptyPixelColor;
            PixelChunk chunk = chunkmap.chunks[chunkmap.GetOuterChunkPos(position)];
            return chunk.texture.GetPixel(innerChunkPos.x, innerChunkPos.y);
        }

        /// <summary>
        /// Gets the colors of all pixels at a given position.
        /// </summary>
        /// <param name="positions">The positions of the pixels to get.</param>
        /// <returns>The color of the pixels.</returns>
        public virtual Color[] GetPixels(Vector2Int[] positions)
        {
            NativeArray<Color32> n_colors = new NativeArray<Color32>(
                positions.Length,
                Allocator.TempJob
            );
            for (int i = 0; i < n_colors.Length; i++)
                n_colors[i] = emptyPixelColor;
            Jobs.EditJobScedule<NativeArray<Color32>, Jobs.GetPixelsJob>(positions, n_colors, this);
            Color32[] colors = new Color32[n_colors.Length];
            n_colors.CopyTo(colors);
            n_colors.Dispose();
            return _ColorUtility.ConvertColor32ArrToColorArr(colors);
        }
        #endregion
        #region Clearers
        /// <summary>
        /// Clears the pixelmap.
        /// </summary>
        /// <param name="apply">Whether to apply the change to the underlying textures.</param>
        public virtual void ClearAll(bool apply)
        {
            foreach (KeyValuePair<Vector2Int, PixelChunk> k in chunkmap.chunks)
            {
                if (k.Value.texture == null)
                    return;
                Graphics.CopyTexture(_emptyTexture, k.Value.texture);
                if (apply)
                    k.Value.texture.Apply();
                MapChangeCallbackForChunkTextureModification(k.Key, k.Value.texture);
            }
        }

        /// <summary>
        /// Totally deletes all chunks.
        /// </summary>
        public virtual void HardReset()
        {
            foreach (KeyValuePair<Vector2Int, PixelChunk> k in chunkmap.chunks)
                Destroy(k.Value.gameObject);
            chunkmap.chunks.Clear();
            mapClearCallback?.Invoke();
        }
        #endregion
        #region ChunkEditing
        /// <summary>
        /// Adds a chunk to the map at a given position.
        /// </summary>
        /// <param name="chunkPosition">The position of the new chunk.</param>
        /// <param name="changeCallback">Whether this should trigger the change callback.</param>
        /// <returns>A reference to the recently added chunk.</returns>
        public virtual PixelChunk AddChunk(Vector2Int chunkPosition, bool changeCallback = true) =>
            AddChunk(chunkPosition, _emptyTexture, changeCallback);

        /// <summary>
        /// Adds a chunk to the map at a given position.
        /// </summary>
        /// <param name="chunkPosition">The position of the new chunk.</param>
        /// <param name="tex">The texture to initialize this chunk with.</param>
        /// <param name="changeCallback">Whether this should trigger the change callback.</param>
        /// <returns>A reference to the recently added chunk.</returns>
        public virtual PixelChunk AddChunk(
            Vector2Int chunkPosition,
            Texture2D tex,
            bool changeCallback = true
        )
        {
            PixelChunk chunk = base.AddChunk(chunkPosition);
            if (chunk == null)
                return null;
            chunk.Init(
                chunkmap.chunkSize,
                _sortLayer,
                _sortOrder,
                _transparency,
                tex,
                pixelsPerUnit
            );
            if (changeCallback)
                MapChangeCallbackForChunkTextureModification(chunkPosition, tex);
            return chunk;
        }

        /// <summary>
        /// Set the texture at any existing chunk.
        /// </summary>
        /// <param name="chunkPosition">The position of the chunk.</param>
        /// <param name="tex">The texture to set it to.</param>
        public virtual void SetChunkTexture(Vector2Int chunkPosition, Texture2D tex)
        {
            if (tex == null)
                return;
            AddChunk(chunkPosition);
            chunkmap.GetChunk(chunkPosition).SetTexture(tex);
            MapChangeCallbackForChunkTextureModification(chunkPosition, tex);
        }

        /// <summary>
        /// Get a reference to the texture at any given chunk position.
        /// </summary>
        /// <param name="chunkPosition">The position of the chunk.</param>
        /// <returns>A reference to that chunks texture.</returns>
        public virtual Texture2D GetChunkTexture(Vector2Int chunkPosition) =>
            chunkmap?.GetChunk(chunkPosition)?.texture;

        /// <summary>
        /// When an entire chunk texture is edited, not the inner pixels, this manages the callback for those changes.
        /// </summary>
        /// <param name="chunkPosition">The position of the chunk the changes occured to.</param>
        /// <param name="texture">The new texture this chunk received.</param>
        public void MapChangeCallbackForChunkTextureModification(
            Vector2Int chunkPosition,
            Texture2D texture
        )
        {
            if (!shouldCallbackChanges)
                return;
            (Vector2Int[], Color[]) Pixels = PixelConversionUtility.GetPixelsFromTexture2D(
                texture,
                emptyPixelColor
            );
            Pixels.Item1 = VectorMath.TranslateVectorArray(
                Pixels.Item1,
                chunkPosition * (int)chunkmap.chunkSize
            );
            mapChangeCallback?.Invoke(Pixels.Item1, Pixels.Item2, this);
        }

        /// <summary>
        /// Apply the texture changes at any given chunk.
        /// This is usually done automatically, but if you are managing texture
        /// application in a custom way this allows for that.
        /// </summary>
        /// <param name="position">The position of the chunk to apply the texture at.</param>
        public virtual void Apply(Vector2Int position)
        {
            Vector2Int chunkPosition = chunkmap.GetOuterChunkPos(position);
            AddChunk(chunkPosition);
            PixelChunk chunk = chunkmap.GetChunk(chunkPosition);
            chunk?.texture.Apply();
        }

        /// <summary>
        /// For each Vector2Int to Texture pairing,
        /// the chunk at that position with that texture.
        /// </summary>
        /// <param name="data">The data to apply to this map.</param>
        public virtual void ApplyData(Dictionary<Vector2Int, Texture2D> data)
        {
            foreach (KeyValuePair<Vector2Int, Texture2D> k in data)
            {
                k.Value.name = k.Key.ToString();
                AddChunk(k.Key, k.Value);
            }
        }
        #endregion
    }
}
