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
            NativeArray<Vector2Int> returned;
            if (!unique)
                returned = ChunkMapInt2DJobs.GetChunksForPositionsJobWrapped(positions, chunkSizeV2I, innerLoopBatchCount, allocation);
            else
                returned = ChunkMapInt2DJobs.GetUniqueChunksForPositionsJobWrapped(positions, chunkSizeV2I, allocation);
            return returned;
        }
        private static partial class ChunkMapInt2DJobs
        {
            #region GetChunksForPositions
            public static NativeArray<Vector2Int> GetChunksForPositionsJobWrapped(NativeArray<Vector2Int> positions, Vector2Int chunkSize, int innerLoopBatchCount, Allocator allocation)
            {
                NativeArray<Vector2Int> chunks = new NativeArray<Vector2Int>(positions.Length, allocation);

                GetChunksForPositionsJob job = new GetChunksForPositionsJob()
                {
                    positions = positions,
                    chunks = chunks,
                    chunkSize = chunkSize
                };
                int batchCount = innerLoopBatchCount > 0 ? innerLoopBatchCount : positions.Length / 10;
                if (batchCount == 0)
                    batchCount = 1;
                JobHandle jobHandle = job.Schedule(positions.Length, batchCount);
                jobHandle.Complete();
                return chunks;
            }

            [BurstCompile]
            struct GetChunksForPositionsJob : IJobParallelFor
            {
                public NativeArray<Vector2Int> positions;
                public NativeArray<Vector2Int> chunks;
                public Vector2Int chunkSize;
                public void Execute(int index)
                {
                    Vector2 vector = (Vector2)positions[index] / chunkSize;
                    chunks[index] = new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
                }
            }
            #endregion
            #region UniqueChunkPositions
            public static NativeArray<Vector2Int> GetUniqueChunksForPositionsJobWrapped(NativeArray<Vector2Int> positions, Vector2Int chunkSize, Allocator allocation)
            {
                
                NativeList<Vector2Int> chunks = new NativeList<Vector2Int>(Allocator.TempJob);
                GetUniqueChunksForPositionsJob job = new GetUniqueChunksForPositionsJob()
                {
                    positions = positions,
                    chunks = chunks,
                    chunkSize = chunkSize
                };
                JobHandle jobHandle = job.Schedule();
                jobHandle.Complete();
                NativeArray<Vector2Int> n_chunks = new NativeArray<Vector2Int>(chunks, allocation);
                chunks.Dispose();
                return n_chunks;
            }
            [BurstCompile]
            struct GetUniqueChunksForPositionsJob : IJob
            {
                public NativeArray<Vector2Int> positions;
                public NativeList<Vector2Int> chunks;
                public Vector2Int chunkSize;
                public void Execute()
                {
                    for (int index = 0; index < positions.Length; index++)
                    {
                        Vector2 vector = (Vector2)positions[index] / chunkSize;
                        Vector2Int chunk = new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
                        if (chunks.Contains(chunk))
                            continue;
                        chunks.Add(chunk);
                    }
                }
            }
            #endregion
        }
    }
}