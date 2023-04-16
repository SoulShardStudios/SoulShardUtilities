using UnityEngine;
using SoulShard.Math;

namespace SoulShard.Utils
{
    /// <summary>
    /// this is an external container for the math behind converting chunk positions
    /// </summary>
    public struct ChunkPositionConversions
    {
        public static Vector2Int GetOuterChunkPos(Vector2Int position, Vector2Int size)
        {
            var outerPos = (Vector2)position / size;
            return new Vector2Int(Mathf.FloorToInt(outerPos.x), Mathf.FloorToInt(outerPos.y));
        }

        public static Vector2Int GetInnerChunkPos(Vector2Int position, Vector2Int size)
        {
            return new Vector2Int(
                MathUtility.PositiveMod(position.x, size.x),
                MathUtility.PositiveMod(position.y, size.x)
            );
        }

        public static ChunkPosition GetChunkPos(Vector2Int position, Vector2Int size) =>
            new ChunkPosition(GetOuterChunkPos(position, size), GetInnerChunkPos(position, size));
    }
}
