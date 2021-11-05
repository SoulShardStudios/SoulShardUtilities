using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using System.Linq;
namespace SoulShard.Utils
{
    // Because converting chunk positions for a huge array for a chunkmap
    // is very slow in raw C# format, doing it in a unity job is much faster.
    // this entire file is just for efficiency's sake
    // however its alot harder to do and this file shows that. some really jank code here
    // I could not find a clean way to not repeat alot of job code
    // I did as much as I could with scheduling but thats all.
    public partial class ChunkMapInt2D<T>
    {
        interface IChunkJob<_returnType> where _returnType : struct
        {
            public void Init(_returnType @return, NativeArray<Vector2Int> positions, Vector2Int chunkSize);
            public _returnType GenerateOutput(int length, Allocator allocation);
        }
        #region JobHandling
        public Vector2Int[] ConvertToInnerChunkPositionsJob(Vector2Int[] positions) =>
            ConvertToSingleChunkPositionsJob<Jobs.InnerChunkPositionConversionJob>(positions);
        public Vector2Int[] ConvertToOuterChunkPositionsJob(Vector2Int[] positions) =>
            ConvertToSingleChunkPositionsJob<Jobs.OuterChunkPositionConversionJob>(positions);
        public Vector2Int[] ConvertToOuterChunkPositionsJobUnique(Vector2Int[] positions) =>
            ConvertToSingleChunkPositionsJob<Jobs.OuterChunkPositionConversionJob>(positions).Distinct().ToArray();
        public ChunkPosition[] ConvertToChunkPositionsJob(Vector2Int[] positions)
        {
            ChunkPosition[] @return = new ChunkPosition[positions.Length];
            NativeArray<Vector2Int> n_positions = new NativeArray<Vector2Int>(positions, Allocator.TempJob);
            NativeArray<ChunkPosition> n_inners = Jobs.StandardParallelChunkJob
                <Jobs.ChunkPositionConversionJob, NativeArray<ChunkPosition>>(n_positions, chunkSizeV2I);
            n_inners.CopyTo(@return);
            n_inners.Dispose();
            n_positions.Dispose();
            return @return;
        }
        Vector2Int[] ConvertToSingleChunkPositionsJob<_JobType>(Vector2Int[] positions) where _JobType: struct, IJobParallelFor, IChunkJob<NativeArray<Vector2Int>>
        {
            Vector2Int[] @return = new Vector2Int[positions.Length];
            NativeArray<Vector2Int> n_positions = new NativeArray<Vector2Int>(positions, Allocator.TempJob);
            NativeArray<Vector2Int> n_inners = Jobs.StandardParallelChunkJob
                <_JobType, NativeArray<Vector2Int>>(n_positions, chunkSizeV2I);
            n_inners.CopyTo(@return);
            n_inners.Dispose();
            n_positions.Dispose();
            return @return;
        }
        #endregion
        static class Jobs
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
                    @return[index] = Conversions.GetOuterChunkPos(positions[index], chunkSize);
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
                    @return[index] = Conversions.GetInnerChunkPos(positions[index], chunkSize);
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
                    @return[index] = Conversions.GetChunkPos(positions[index], chunkSize);
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
            #endregion
        }
    }
}