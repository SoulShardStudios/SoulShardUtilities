using UnityEngine;
using System.Collections.Generic;
using System;
namespace SoulShard.Utils
{
    /// <summary>
    /// allows for management and math related to an infinitely tiled 2d plane of chunks (as monobehaviors), with vector2Int used as the positioning method
    /// </summary>
    /// <typeparam name="_chunkType"> the monobehavior component type of the chunk </typeparam>
    [Serializable]
    public partial class ChunkMapInt2D<_chunkType>
        where _chunkType : MonoBehaviour
    {
        #region Vars
        /// <summary>
        /// the collection of chunks
        /// </summary>
        [HideInInspector] public Dictionary<Vector2Int, _chunkType> chunks = new Dictionary<Vector2Int, _chunkType>();
        #region chunkSize
        /// <summary>
        /// the size of an individual chunk
        /// </summary>
        public uint chunkSize;
        /// <summary>
        /// the vector2int representation of the chunksize
        /// </summary>
        public Vector2Int chunkSizeV2I { get => new Vector2Int((int)chunkSize, (int)chunkSize); }
        #endregion
        /// <summary>
        /// pixels per unit that the chunks are using
        /// </summary>
        [HideInInspector] public int PPU = 1;
        /// <summary>
        /// should Gizmos for thechunk borders be drawn?
        /// </summary>
        public bool drawChunkBorders;
        /// <summary>
        /// the color for the chunk border gizmos
        /// </summary>
        public Color chunkBorderColor;

        #endregion
        #region Constructors
        public ChunkMapInt2D(Color chunkBorderColor, uint chunkSize = 1, bool drawChunkBorders = true)
        {
            this.chunkSize = chunkSize;
            this.chunkBorderColor = chunkBorderColor;
            this.drawChunkBorders = drawChunkBorders;
        }
        public ChunkMapInt2D(uint chunkSize = 1, bool drawChunkBorders = true)
        {
            this.chunkSize = chunkSize;
            chunkBorderColor = Color.white;
            this.drawChunkBorders = drawChunkBorders;
        }
        #endregion
        #region Funcs
        /// <summary>
        /// draws boundaries on all the chunks for debugging
        /// </summary>
        public void DrawGizmoBorders()
        {
            Gizmos.color = chunkBorderColor;
            if (!drawChunkBorders)
                return;
            foreach (KeyValuePair<Vector2Int, _chunkType> k in chunks)
                GizmosUtility.DrawRect(new Rect(k.Value.transform.position, chunkSizeV2I), PPU, k.Value.gameObject.transform.position);
        }
        /// <summary>
        /// gets a specific chunk. this is needed to make sure idiots don't cause a keynotfoundexception when acessing the dictionary
        /// </summary>
        /// <param name="position"> the position of the desired chunk </param>
        /// <returns> the desired chunk </returns>
        public _chunkType GetChunk(Vector2Int position)
        {
            try { return chunks[position]; }
            catch (KeyNotFoundException) { return null; }
        }
        #endregion
    }
}