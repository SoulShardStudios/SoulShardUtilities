using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
namespace SoulShard.Utils
{
    public partial class ChunkMapInt2D<T>
    {
        public Vector2Int[] GetChunksForPositionsJob(Vector2Int[] positions, bool unique = false)
        {
            NativeArray<Vector2Int> n_positions = new NativeArray<Vector2Int>(positions, Allocator.TempJob);
            NativeArray<Vector2Int> n_chunks = GetChunksForPositionsJob(n_positions, allocation: Allocator.TempJob, unique: unique);
            Vector2Int[] chunks = new Vector2Int[n_chunks.Length];
            n_chunks.CopyTo(chunks);
            n_chunks.Dispose();
            n_positions.Dispose();
            return chunks;
        }
        public NativeArray<Vector2Int> GetChunksForPositionsJob(NativeArray<Vector2Int> positions, int innerLoopBatchCount = -1, Allocator allocation = Allocator.TempJob, bool unique = false)
        {
            NativeArray<Vector2Int> @return;
            if (!unique)
                @return = ChunkMapInt2DJobs.StandardParallelChunkJob
                    <ChunkMapInt2DJobs.GetChunksForPositionsJob, NativeArray<Vector2Int>>
                    (positions, chunkSizeV2I, innerLoopBatchCount, allocation);
            else
                @return = ChunkMapInt2DJobs.StandardChunkJob
                    <ChunkMapInt2DJobs.GetUniqueChunksForPositionsJob, NativeList<Vector2Int>>
                    (positions, chunkSizeV2I, allocation);
            return @return;
        }
        private static partial class ChunkMapInt2DJobs
        {
            [BurstCompile]
            public struct GetChunksForPositionsJob : IJobParallelFor, IChunkJob<NativeArray<Vector2Int>>
            {
                NativeArray<Vector2Int> positions;
                NativeArray<Vector2Int> @return;
                Vector2Int chunkSize;
                public void Init(NativeArray<Vector2Int> @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize)
                {
                    this.@return = @return;
                    this.positions = positions;
                    this.chunkSize = chunkSize;
                }
                public NativeArray<Vector2Int> GenerateOutput(int length, Allocator allocation) =>
                    new NativeArray<Vector2Int>(positions.Length, allocation);
                public void Execute(int index)
                {
                    Vector2 vector = (Vector2)positions[index] / chunkSize;
                    @return[index] = new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
                }
            }
            [BurstCompile]
            public struct GetUniqueChunksForPositionsJob : IJob, IChunkJob<NativeList<Vector2Int>>
            {
                NativeArray<Vector2Int> positions;
                NativeList<Vector2Int> @return;
                Vector2Int chunkSize;
                public NativeList<Vector2Int> GenerateOutput(int length, Allocator allocation) =>
                    new NativeList<Vector2Int>(length, allocation);
                public void Init(NativeList<Vector2Int> @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize)
                {
                    this.positions = positions;
                    this.chunkSize = chunkSize;
                    this.@return = @return;
                }
                public void Execute()
                {
                    for (int index = 0; index < positions.Length; index++)
                    {
                        Vector2 vector = (Vector2)positions[index] / chunkSize;
                        Vector2Int chunk = new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
                        if (@return.Contains(chunk))
                            continue;
                        @return.Add(chunk);
                    }
                }
            }
        }
    }
}