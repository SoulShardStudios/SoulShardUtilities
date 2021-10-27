using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
namespace SoulShard.Utils
{
    public partial class ChunkMapInt2D<T>
    {
        private static partial class ChunkMapInt2DJobs
        {
            static int GetBatchAmount(int size, int partitions, int manualPartition)
            {
                int batchCount = manualPartition > 0 ? manualPartition : size / partitions;
                if (batchCount == 0)
                    batchCount = 1;
                return batchCount;
            }
            public static _jobOutputType StandardParallelChunkJob
                <_T, _jobOutputType>
                (NativeArray<Vector2Int> positions, Vector2Int chunkSize, int innerLoopBatchCount, Allocator allocation)
                where _T : struct, IJobParallelFor, IChunkJob<_jobOutputType>
            {
                _T job = new _T();
                _jobOutputType @return = job.GenerateOutput(positions.Length, allocation);
                job.Init(@return, positions, chunkSize);
                JobHandle jobHandle = job.Schedule(positions.Length, GetBatchAmount(positions.Length, 10, innerLoopBatchCount));
                jobHandle.Complete();
                return @return;
            }
            public static _jobOutputType StandardChunkJob
                <_T, _jobOutputType>
                (NativeArray<Vector2Int> positions, Vector2Int chunkSize, Allocator allocation)
                where _T : struct, IJob, IChunkJob<_jobOutputType>
            {
                _T job = new _T();
                _jobOutputType @return = job.GenerateOutput(positions.Length, allocation);
                job.Init(@return, positions, chunkSize);
                JobHandle jobHandle = job.Schedule();
                jobHandle.Complete();
                return @return;
            }
        }
    }
}