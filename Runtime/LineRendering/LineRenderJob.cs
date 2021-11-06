using UnityEngine;
using Unity.Collections;
using Unity.Burst;
using Unity.Jobs;
namespace SoulShard.Utils
{
    public partial struct LineRenderUtility
    {
        public static Vector2Int[] InterpolateLine(Vector2Int pos0, Vector2Int pos1)
        {
            NativeList<Vector2Int> native = new NativeList<Vector2Int>(0, Allocator.TempJob); ;
            Job Job = new Job()
            {
                pos0 = pos0,
                pos1 = pos1,
                @return = native,
            };
            JobHandle handle = Job.Schedule();
            handle.Complete();
            Vector2Int[] @return = native.ToArray();
            native.Dispose();
            return @return;
        }

        [BurstCompile]
        struct Job : IJob
        {
            public Vector2Int pos0, pos1;
            public NativeList<Vector2Int> @return;
            public void Execute()
            {
                LineRenderVars v = LineRenderPreProcessing(pos0, pos1);
                for (int x = v.pos0.x; x <= v.pos1.x; x++)
                {
                    @return.Add(new Vector2Int(v.steep ? v.y : x, v.steep ? x : v.y));
                    v.error -= v.dy;
                    if (v.error < 0)
                    {
                        v.y += v.ystep;
                        v.error += v.dx;
                    }
                }
            }
        }
    }
}
