using UnityEngine;
using Unity.Collections;
namespace SoulShard.Utils
{
    public partial class ChunkMapInt2D<T>
    {
        interface IChunkJob<_returnType> where _returnType: struct
        {
            public void Init(_returnType @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize);
            public _returnType GenerateOutput(int length, Allocator allocation);
        }
    }
}