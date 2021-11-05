using UnityEngine;
using System;
namespace SoulShard.Utils
{
    // this stores the chunks world position, and the position within the chunk together.
    public struct ChunkPosition : IEquatable<ChunkPosition>, IFormattable
    {
        public Vector2Int outer;
        public Vector2Int inner;
        public ChunkPosition(Vector2Int outer, Vector2Int inner)
        {
            this.outer = outer;
            this.inner = inner;
        }
        public bool Equals(ChunkPosition other)
        {
            return other.outer.x == outer.x &&
                other.outer.y == outer.y &&
                other.inner.x == inner.x &&
                other.inner.y == inner.y;
        }
        public string ToString(string format, IFormatProvider formatProvider) =>
            (outer, inner).ToString();
    }
}