using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using SoulShard.Utils;
using Unity.Collections.LowLevel.Unsafe;

namespace SoulShard.PixelMaps
{
    public partial class PixelMap
    {
        /// <summary>
        /// This contains all of the unity jobs acceleration for the pixel map
        /// I'm making it separate due to the fact that its quite messy...
        /// </summary>
        static class Jobs
        {
            public static _inputType EditJobScedule<_inputType, _jobType>(
                Vector2Int[] positions,
                _inputType input,
                PixelMap map
            ) where _jobType : struct, IPixelMapEditJob<_inputType>, IJobParallelFor
            {
                NativeArray<ChunkPosition> n_positions = new NativeArray<ChunkPosition>(
                    map.chunkmap.ConvertToChunkPositionsJob(positions),
                    Allocator.TempJob
                );
                Vector2Int[] chunks = map.chunkmap.ConvertToOuterChunkPositionsJobUnique(positions);
                NativeArray<JobHandle> jobs = new NativeArray<JobHandle>(
                    chunks.Length,
                    Allocator.TempJob
                );
                // For all of the chucnks that need to be edited.
                for (int i = 0; i < chunks.Length; i++)
                {
                    // If the chunk does not already exist, add it.
                    map.AddChunk(chunks[i]);
                    // Get the texture.
                    NativeArray<Color32> texture = map.chunkmap
                        .GetChunk(chunks[i])
                        .texture.GetRawTextureData<Color32>();
                    // Run the associated job.
                    _jobType job = new _jobType();
                    job.Init(input, n_positions, texture, map.chunkmap.chunkSizeV2I, chunks[i]);
                    jobs[i] = job.Schedule(
                        n_positions.Length,
                        JobUtility.GetBatchAmount(n_positions.Length, 10, -1)
                    );
                }
                // Wait for all jobs to complete then return the response.
                JobHandle.CompleteAll(jobs);
                n_positions.Dispose();
                jobs.Dispose();
                // Apply all the texture edits.
                for (int i = 0; i < chunks.Length; i++)
                    map.chunkmap.GetChunk(chunks[i]).texture.Apply();
                return input;
            }

            public interface IPixelMapEditJob<inputType>
            {
                public void Init(
                    inputType @input,
                    NativeArray<ChunkPosition> positions,
                    NativeArray<Color32> texture,
                    Vector2Int chunksize,
                    Vector2Int textureChunkPos
                );
            }

            #region JobStructs
            [BurstCompile]
            public struct SetPixelsJob : IJobParallelFor, IPixelMapEditJob<NativeArray<Color32>>
            {
                [NativeDisableParallelForRestriction]
                NativeArray<Color32> texture;

                [ReadOnly]
                Vector2Int textureChunkPos;

                [ReadOnly]
                NativeArray<Color32> colors;

                [ReadOnly]
                NativeArray<ChunkPosition> positions;

                [ReadOnly]
                Vector2Int chunksize;

                public void Init(
                    NativeArray<Color32> colors,
                    NativeArray<ChunkPosition> positions,
                    NativeArray<Color32> texture,
                    Vector2Int chunksize,
                    Vector2Int textureChunkPos
                )
                {
                    this.colors = colors;
                    this.positions = positions;
                    this.texture = texture;
                    this.textureChunkPos = textureChunkPos;
                    this.chunksize = chunksize;
                }

                public void Execute(int index)
                {
                    if (positions[index].outer != textureChunkPos)
                        return;
                    Vector2Int pos = positions[index].inner;
                    texture[CollectionUtility.GetIndex(pos, chunksize.x)] = colors[index];
                }
            }

            [BurstCompile]
            public struct SetPixelsJobSingleColor : IJobParallelFor, IPixelMapEditJob<Color32>
            {
                [NativeDisableParallelForRestriction]
                NativeArray<Color32> texture;

                [ReadOnly]
                Vector2Int textureChunkPos;

                [ReadOnly]
                Color32 color;

                [ReadOnly]
                NativeArray<ChunkPosition> positions;

                [ReadOnly]
                Vector2Int chunksize;

                public void Init(
                    Color32 color,
                    NativeArray<ChunkPosition> positions,
                    NativeArray<Color32> texture,
                    Vector2Int chunksize,
                    Vector2Int textureChunkPos
                )
                {
                    this.color = color;
                    this.positions = positions;
                    this.texture = texture;
                    this.textureChunkPos = textureChunkPos;
                    this.chunksize = chunksize;
                }

                public void Execute(int index)
                {
                    if (positions[index].outer != textureChunkPos)
                        return;
                    Vector2Int pos = positions[index].inner;
                    texture[CollectionUtility.GetIndex(pos, chunksize.x)] = color;
                }
            }

            [BurstCompile]
            public struct GetPixelsJob : IJobParallelFor, IPixelMapEditJob<NativeArray<Color32>>
            {
                [NativeDisableContainerSafetyRestriction]
                [NativeDisableParallelForRestriction]
                NativeArray<Color32> colors;

                [ReadOnly]
                NativeArray<Color32> texture;

                [ReadOnly]
                Vector2Int textureChunkPos;

                [ReadOnly]
                NativeArray<ChunkPosition> positions;

                [ReadOnly]
                Vector2Int chunksize;

                public void Init(
                    NativeArray<Color32> colors,
                    NativeArray<ChunkPosition> positions,
                    NativeArray<Color32> texture,
                    Vector2Int chunksize,
                    Vector2Int textureChunkPos
                )
                {
                    this.colors = colors;
                    this.positions = positions;
                    this.texture = texture;
                    this.textureChunkPos = textureChunkPos;
                    this.chunksize = chunksize;
                }

                public void Execute(int index)
                {
                    if (positions[index].outer != textureChunkPos)
                        return;
                    Vector2Int pos = positions[index].inner;
                    colors[index] = texture[CollectionUtility.GetIndex(pos, chunksize.x)];
                }
            }
            #endregion
        }
    }
}
