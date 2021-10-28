using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
namespace SoulShard.Utils
{
    public partial class ChunkMapInt2D<T>
    {
        public Vector2Int[] GetInnerChunkPositionsForWorldPositionsJob(Vector2Int[] positions)
        {
            Vector2Int[] @return = new Vector2Int[positions.Length];
            NativeArray<Vector2Int> n_positions = new NativeArray<Vector2Int>(positions, Allocator.TempJob);
            NativeArray<Vector2Int> n_inners = GetInnerChunkPositionsForWorldPositionsJob(n_positions);
            n_inners.CopyTo(@return);
            n_inners.Dispose();
            n_positions.Dispose();
            return @return;
        }
        public NativeArray<Vector2Int> GetInnerChunkPositionsForWorldPositionsJob(NativeArray<Vector2Int> positions, int innerLoopBatchCount = -1, Allocator allocation = Allocator.TempJob)
        {
            return ChunkMapInt2DJobs.StandardParallelChunkJob
                <ChunkMapInt2DJobs.GetInnerChunkPositionsForWorldPositionsJob, NativeArray<Vector2Int>>
                (positions, chunkSizeV2I, innerLoopBatchCount, allocation);
        }
        private static partial class ChunkMapInt2DJobs
        {
            [BurstCompile]
            public struct GetInnerChunkPositionsForWorldPositionsJob : IJobParallelFor, IChunkJob<NativeArray<Vector2Int>>
            {
                NativeArray<Vector2Int> @return;
                NativeArray<Vector2Int> positions;
                int chunkSize;
                Vector2Int chunkSizeV2I;
                public NativeArray<Vector2Int> GenerateOutput(int length, Allocator allocation) =>
                    new NativeArray<Vector2Int>(length, allocation);
                public void Init(NativeArray<Vector2Int> chunks, NativeArray<Vector2Int> positions, Vector2Int chunkSize)
                {
                    this.positions = positions;
                    this.chunkSize = chunkSize.x;
                    chunkSizeV2I = chunkSize;
                    @return = chunks;
                }
                public void Execute(int index)
                {
                    int x = positions[index].x % chunkSize;
                    int y = positions[index].y % chunkSize;
                    @return[index] = new Vector2Int(x, y) + chunkSizeV2I / 2;
                }
            }
        }
    }
}