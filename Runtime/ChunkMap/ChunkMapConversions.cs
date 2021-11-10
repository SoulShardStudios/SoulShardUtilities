using UnityEngine;
namespace SoulShard.Utils
{
    // allows conversion between world positions and chunk positions.
    // essentially you can split chunk positions into the position of the chunk itself and the position inside the chunk
    // inner is inside, outer is outside. you can convert between world and those two or do both at once.
    public partial class ChunkMapInt2D<_chunkType> where _chunkType : MonoBehaviour
    {
        #region Single Item Public Getters
        /// <summary>
        /// gets the outer chunk position for the input world position
        /// </summary>
        /// <param name="position"> the position to convert </param>
        /// <returns></returns>
        public Vector2Int GetOuterChunkPos(Vector2Int position) => 
            Conversions.GetOuterChunkPos(position, chunkSizeV2I);
        /// <summary>
        /// gets the inner chunk position for the input world position
        /// </summary>
        /// <param name="position"> the position to convert </param>
        /// <returns></returns>
        public Vector2Int GetInnerChunkPos(Vector2Int position) => 
            Conversions.GetInnerChunkPos(position, chunkSizeV2I);
        /// <summary>
        /// gets the chunk position for the input world position
        /// </summary>
        /// <param name="position"> the position to convert </param>
        /// <returns></returns>
        public ChunkPosition GetChunkPos(Vector2Int position) => 
            Conversions.GetChunkPos(position, chunkSizeV2I);
        #endregion
        struct Conversions
        {
            public static Vector2Int GetOuterChunkPos(Vector2Int position, Vector2Int size) => 
                VectorMath.RoundVector((Vector2)position / size);
            public static Vector2Int GetInnerChunkPos(Vector2Int position, Vector2Int size) => 
                VectorMath.PositiveModVector(position + size / 2, size.x);
            public static ChunkPosition GetChunkPos(Vector2Int position, Vector2Int size) => 
                new ChunkPosition(GetOuterChunkPos(position, size), GetInnerChunkPos(position, size));
        }
    }
}