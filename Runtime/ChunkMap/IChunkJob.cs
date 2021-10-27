using UnityEngine;
using Unity.Collections;
namespace SoulShard.Utils
{
    public partial class ChunkMapInt2D<T>
    {
        interface IChunkJob<_T>
        {
            public void Init(_T @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize);
            public _T GenerateOutput(int length, Allocator allocation);
        }
    }
}