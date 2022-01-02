using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
namespace SoulShard.Utils
{
    using Internal;
    /// <summary>
    /// allows for management and math related to an infinitely tiled 2d plane of chunks (as monobehaviors), with vector2Int used as the positioning method
    /// </summary>
    /// <typeparam name="_chunkType"> the monobehavior component type of the chunk </typeparam>
    [Serializable]
    public class ChunkMapInt2D<_chunkType> where _chunkType: class
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
        #endregion
        public ChunkMapInt2D(uint chunkSize = 1)
        {
            this.chunkSize = chunkSize;
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

        #region All Chunk Position Conversions
        #region Single Conversions
        /// <summary>
        /// gets the outer chunk position for the input world position
        /// </summary>
        /// <param name="position"> the position to convert </param>
        /// <returns></returns>
        public Vector2Int GetOuterChunkPos(Vector2Int position) =>
            ChunkPositionConversions.GetOuterChunkPos(position, chunkSizeV2I);
        /// <summary>
        /// gets the inner chunk position for the input world position
        /// </summary>
        /// <param name="position"> the position to convert </param>
        /// <returns></returns>
        public Vector2Int GetInnerChunkPos(Vector2Int position) =>
            ChunkPositionConversions.GetInnerChunkPos(position, chunkSizeV2I);
        /// <summary>
        /// gets the chunk position for the input world position
        /// </summary>
        /// <param name="position"> the position to convert </param>
        /// <returns></returns>
        public ChunkPosition GetChunkPos(Vector2Int position) =>
            ChunkPositionConversions.GetChunkPos(position, chunkSizeV2I);
        #endregion
        #region Job Conversions
        /// <summary>
        /// converts an array of world positions to an array of inner chunk positions using unity jobs
        /// </summary>
        /// <param name="positions"> the positions to convert </param>
        /// <returns> the converted inner chunk positions </returns>
        public Vector2Int[] ConvertToInnerChunkPositionsJob(Vector2Int[] positions) =>
            ChunkPositionJobs.ConvertToSingleChunkPositionsJob<ChunkPositionJobs.InnerChunkPositionConversionJob>(positions, chunkSizeV2I);
        /// <summary>
        /// converts an array of world positions to an array of outer chunk positions using unity jobs
        /// </summary>
        /// <param name="positions"> the positions to convert </param>
        /// <returns> the converted outer chunk positions </returns>
        public Vector2Int[] ConvertToOuterChunkPositionsJob(Vector2Int[] positions) =>
            ChunkPositionJobs.ConvertToSingleChunkPositionsJob<ChunkPositionJobs.OuterChunkPositionConversionJob>(positions, chunkSizeV2I);
        /// <summary>
        /// converts an array of world positions to an array of unique outer chunk positions using unity jobs
        /// </summary>
        /// <param name="positions"> the positions to convert </param>
        /// <returns> the converted outer chunk positions </returns>
        public Vector2Int[] ConvertToOuterChunkPositionsJobUnique(Vector2Int[] positions) =>
            ChunkPositionJobs.ConvertToSingleChunkPositionsJob<ChunkPositionJobs.OuterChunkPositionConversionJob>(positions, chunkSizeV2I).Distinct().ToArray();
        #endregion
        #endregion
    }
}