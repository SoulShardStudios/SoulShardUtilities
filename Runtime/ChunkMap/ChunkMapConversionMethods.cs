using UnityEngine;
using SoulShard.Math;
namespace SoulShard.Utils
{
    /// <summary>
    /// this is an external container for the math behind converting chunk positions
    /// </summary>
    public struct ChunkPositionConversions
    {
        public static Vector2Int GetOuterChunkPos(Vector2Int position, Vector2Int size) =>
            VectorMath.FloorVector((Vector2)position / size);
        public static Vector2Int GetInnerChunkPos(Vector2Int position, Vector2Int size) =>
            VectorMath.PositiveModVector(position, size.x);
        public static ChunkPosition GetChunkPos(Vector2Int position, Vector2Int size) =>
            new ChunkPosition(GetOuterChunkPos(position, size), GetInnerChunkPos(position, size));
    }
}