using UnityEngine;
using System;
namespace SoulShard.Utils
{
    public struct ChunkPosition : IEquatable<ChunkPosition>, IFormattable
    {
        public Vector2Int chunkPos;
        public Vector2Int innerChunkPos;
        public ChunkPosition(Vector2Int chunkPos, Vector2Int innerChunkPos)
        {
            this.chunkPos = chunkPos;
            this.innerChunkPos = innerChunkPos;
        }
        public bool Equals(ChunkPosition other)
        {
            return other.chunkPos.x == chunkPos.x &&
                other.chunkPos.y == chunkPos.y &&
                other.innerChunkPos.x == innerChunkPos.x &&
                other.innerChunkPos.y == innerChunkPos.y;
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return (chunkPos, innerChunkPos).ToString();
        }
    }
}