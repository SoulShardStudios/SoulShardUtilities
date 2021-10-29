using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
namespace SoulShard.Utils
{
    public partial class ChunkMapInt2D<T>
    {
        private static partial class ChunkMapInt2DJobs
        {
            public static _jobOutputType StandardParallelChunkJob
                <_jobType, _jobOutputType>
                (NativeArray<Vector2Int> positions, Vector2Int chunkSize, int innerLoopBatchCount, Allocator allocation)
                where _jobType : struct, IJobParallelFor, IChunkJob<_jobOutputType>
                where _jobOutputType : struct
            {
                _jobType job = new _jobType();
                _jobOutputType @return = job.GenerateOutput(positions.Length, allocation);
                job.Init(@return, positions, chunkSize);
                JobHandle jobHandle = job.Schedule(positions.Length, JobUtility.GetBatchAmount(positions.Length, 10, innerLoopBatchCount));
                jobHandle.Complete();
                return @return;
            }
            public static _jobOutputType StandardChunkJob
                <_jobType, _jobOutputType>
                (NativeArray<Vector2Int> positions, Vector2Int chunkSize, Allocator allocation)
                where _jobType : struct, IJob, IChunkJob<_jobOutputType>
                where _jobOutputType : struct
            {
                _jobType job = new _jobType();
                _jobOutputType @return = job.GenerateOutput(positions.Length, allocation);
                job.Init(@return, positions, chunkSize);
                JobHandle jobHandle = job.Schedule();
                jobHandle.Complete();
                return @return;
            }
        }
    }
}