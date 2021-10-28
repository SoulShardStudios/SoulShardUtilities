using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
namespace SoulShard.Utils
{
    public partial class ChunkMapInt2D<T>
    {
        public ChunkPosition[] GetChunkMapPositionsJob(Vector2Int[] positions)
        {
            ChunkPosition[] @return = new ChunkPosition[positions.Length];
            NativeArray<ChunkPosition> n_chunkPositions = GetChunkMapPositionsJob(positions, -1, Allocator.TempJob);
            n_chunkPositions.CopyTo(@return);
            n_chunkPositions.Dispose();
            return @return;
        }
        public NativeArray<ChunkPosition> GetChunkMapPositionsJob(Vector2Int[] positions, int innerLoopBatchCount = -1, Allocator allocation = Allocator.TempJob)
        {
            NativeArray<Vector2Int> n_positions = new NativeArray<Vector2Int>(positions, Allocator.TempJob);
            NativeArray<ChunkPosition> n_chunkposses = ChunkMapInt2DJobs.StandardParallelChunkJob
                <ChunkMapInt2DJobs.GetChunkPositionsForWorldPositionsJob, NativeArray<ChunkPosition>>
                (n_positions, chunkSizeV2I, innerLoopBatchCount, allocation);
            n_positions.Dispose();
            return n_chunkposses;
        }
        private static partial class ChunkMapInt2DJobs
        {
            [BurstCompile]
            public struct GetChunkPositionsForWorldPositionsJob : IJobParallelFor, IChunkJob<NativeArray<ChunkPosition>>
            {
                NativeArray<Vector2Int> positions;
                NativeArray<ChunkPosition> @return;
                Vector2Int chunkSizeV2I;
                int chunkSize;
                public NativeArray<ChunkPosition> GenerateOutput(int length, Allocator allocation) =>
                    new NativeArray<ChunkPosition>(length, allocation);
                public void Init(NativeArray<ChunkPosition> @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize)
                {
                    this.@return = @return;
                    this.positions = positions;
                    chunkSizeV2I = chunkSize;
                    this.chunkSize = chunkSize.x;
                }
                public void Execute(int index)
                {
                    // innercalc
                    int x = positions[index].x % chunkSize;
                    int y = positions[index].y % chunkSize;
                    Vector2Int inner = new Vector2Int(x, y) + chunkSizeV2I / 2;
                    // outercalc
                    Vector2 vector = (Vector2)positions[index] / chunkSize;
                    Vector2Int outer = new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
                    @return[index] = new ChunkPosition(outer, inner);
                }
            }
        }
    }
}