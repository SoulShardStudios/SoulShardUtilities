using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
namespace SoulShard.Utils.Internal
{
    // Because converting chunk positions for a huge array for a chunkmap
    // is very slow in raw C# format, doing it in a unity job is much faster.
    // this entire file is just for efficiency's sake
    // however its alot harder to do and this file shows that. some really jank code here
    // I could not find a clean way to not repeat alot of job code
    // I did as much as I could with scheduling but thats all.
    public interface IChunkJob<_returnType> where _returnType : struct
    {
        public void Init(_returnType @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize);
        public _returnType GenerateOutput(int length, Allocator allocation);
    }
    public static class ChunkPositionJobs
    {
        #region Jobs
        [BurstCompile]
        public struct OuterChunkPositionConversionJob : IJobParallelFor, IChunkJob<NativeArray<Vector2Int>>
        {
            NativeArray<Vector2Int> @return;
            NativeArray<Vector2Int> positions;
            Vector2Int chunkSize;
            public NativeArray<Vector2Int> GenerateOutput(int length, Allocator allocation) =>
                new NativeArray<Vector2Int>(length, allocation);
            public void Init(NativeArray<Vector2Int> @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize)
            {
                this.positions = positions;
                this.chunkSize = chunkSize;
                this.@return = @return;
            }
            public void Execute(int index) =>
                @return[index] = ChunkPositionConversions.GetOuterChunkPos(positions[index], chunkSize);
        }
        [BurstCompile]
        public struct InnerChunkPositionConversionJob : IJobParallelFor, IChunkJob<NativeArray<Vector2Int>>
        {
            NativeArray<Vector2Int> @return;
            NativeArray<Vector2Int> positions;
            Vector2Int chunkSize;
            public NativeArray<Vector2Int> GenerateOutput(int length, Allocator allocation) =>
                new NativeArray<Vector2Int>(length, allocation);
            public void Init(NativeArray<Vector2Int> @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize)
            {
                this.positions = positions;
                this.chunkSize = chunkSize;
                this.@return = @return;
            }
            public void Execute(int index) =>
                @return[index] = ChunkPositionConversions.GetInnerChunkPos(positions[index], chunkSize);
        }
        [BurstCompile]
        public struct ChunkPositionConversionJob : IJobParallelFor, IChunkJob<NativeArray<ChunkPosition>>
        {
            NativeArray<ChunkPosition> @return;
            NativeArray<Vector2Int> positions;
            Vector2Int chunkSize;
            public NativeArray<ChunkPosition> GenerateOutput(int length, Allocator allocation) =>
                new NativeArray<ChunkPosition>(length, allocation);
            public void Init(NativeArray<ChunkPosition> @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize)
            {
                this.positions = positions;
                this.chunkSize = chunkSize;
                this.@return = @return;
            }
            public void Execute(int index) =>
                @return[index] = ChunkPositionConversions.GetChunkPos(positions[index], chunkSize);
        }
        #endregion
        #region Execution
        public static _jobOutputType StandardParallelChunkJob
            <_jobType, _jobOutputType>
            (NativeArray<Vector2Int> positions, Vector2Int chunkSize, int innerLoopBatchCount = -1, Allocator allocation = Allocator.TempJob)
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
        public static Vector2Int[] ConvertToSingleChunkPositionsJob<_JobType>(Vector2Int[] positions, Vector2Int chunksize) where _JobType : struct, IJobParallelFor, IChunkJob<NativeArray<Vector2Int>>
        {
            Vector2Int[] @return = new Vector2Int[positions.Length];
            NativeArray<Vector2Int> n_positions = new NativeArray<Vector2Int>(positions, Allocator.TempJob);
            NativeArray<Vector2Int> n_inners = StandardParallelChunkJob
                <_JobType, NativeArray<Vector2Int>>(n_positions, chunksize);
            n_inners.CopyTo(@return);
            n_inners.Dispose();
            n_positions.Dispose();
            return @return;
        }
        #endregion
    }
}