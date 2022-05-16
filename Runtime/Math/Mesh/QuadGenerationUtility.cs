using UnityEngine;
using SoulShard.Math;
using Unity.Collections;

namespace SoulShard.Utils
{
    /// <summary>
    /// allowes for the simplified generation of quads for use in any mesh.
    /// </summary>
    public struct QuadGenerationUtility
    {
        #region Native
        /// <summary>
        /// gets a single scaled, positioned, and sized quad, with nativearray allocation
        /// </summary>
        /// <param name="position">the position of the quad</param>
        /// <param name="scale">the scale of the quad</param>
        /// <param name="size">the size of the quad</param>
        /// <param name="allocation">the allocation for the native arrays</param>
        /// <returns>the generated quad</returns>
        public static NativeArray<Vector3> GetNativeQuad(
            Vector2 position,
            Vector3 scale,
            Vector3 size,
            Allocator allocation
        )
        {
            NativeArray<Vector3> verts = GetNativeQuadPositions(size, allocation);
            for (int i = 0; i < verts.Length; i++)
                verts[i] = VectorMath.MultiplyVector(verts[i] + (Vector3)position, scale);
            return verts;
        }

        static NativeArray<Vector3> GetNativeQuadPositions(Vector3 size, Allocator allocation)
        {
            (Vector3Int, Vector3) @params = GetOrientationAndAdjustedSize(size);
            Vector3Int absOrientation = @params.Item1;
            size = @params.Item2;
            NativeArray<Vector3> verts = new NativeArray<Vector3>(4, allocation);
            if (absOrientation == VectorConstants.zyInt)
            {
                verts[0] = new Vector3(0, 0, size.z);
                verts[1] = new Vector3(0, size.y, size.z);
                verts[2] = Vector3.zero;
                verts[3] = new Vector3(0, size.y, 0);
            }
            if (absOrientation == VectorConstants.zxInt)
            {
                verts[0] = new Vector3(0, 0, size.z);
                verts[1] = new Vector3(size.x, 0, size.z);
                verts[2] = Vector3.zero;
                verts[3] = new Vector3(size.x, 0, 0);
            }
            if (absOrientation == VectorConstants.yxInt)
            {
                verts[0] = new Vector3(0, size.y, 0);
                verts[1] = new Vector3(size.x, size.y, 0);
                verts[2] = Vector3.zero;
                verts[3] = new Vector3(size.x, 0, 0);
            }
            return verts;
        }
        #endregion
        #region Regular
        /// <summary>
        /// gets a single scaled, positioned, and sized quad
        /// </summary>
        /// <param name="position">the position of the quad</param>
        /// <param name="scale">the scale of the quad</param>
        /// <param name="size">the size of the quad</param>
        /// <returns>the generated quad</returns>
        public static (Vector3[], int[]) GetQuad(Vector2 position, Vector3 scale, Vector3 size)
        {
            Vector3[] verts = GetQuadPositions(size);
            verts = VectorMath.TranslateVectorArray(verts, position);
            verts = VectorMath.MultiplyVectorArray(verts, scale);
            return (verts, VectorConstants.QuadIndicies());
        }

        static Vector3[] GetQuadPositions(Vector3 size)
        {
            (Vector3Int, Vector3) @params = GetOrientationAndAdjustedSize(size);
            Vector3Int absOrientation = @params.Item1;
            size = @params.Item2;
            if (absOrientation == VectorConstants.zyInt)
                return new Vector3[]
                {
                    new Vector3(0, 0, size.z),
                    new Vector3(0, size.y, size.z),
                    Vector3.zero,
                    new Vector3(0, size.y, 0)
                };
            if (absOrientation == VectorConstants.zxInt)
                return new Vector3[]
                {
                    new Vector3(0, 0, size.z),
                    new Vector3(size.x, 0, size.z),
                    Vector3.zero,
                    new Vector3(size.x, 0, 0)
                };
            if (absOrientation == VectorConstants.yxInt)
                return new Vector3[]
                {
                    new Vector3(0, size.y, 0),
                    new Vector3(size.x, size.y, 0),
                    Vector3.zero,
                    new Vector3(size.x, 0, 0)
                };
            return null;
        }
        #endregion

        static (Vector3Int, Vector3) GetOrientationAndAdjustedSize(Vector3 size)
        {
            // if a is not zero, set it to either positive one or negative one. this is so the orientation is correct
            #region Orientation Calc
            // I did it this way so that it could be native (for unity jobs)
            // as some peices of code depend on all of this being native compatible
            // I could have just used VectorMath.VectorOperation, but because of this design requirement we get some messy BS
            // note: turn this into intNormalize in the future
            Vector3 rs = VectorMath.RoundVector(size);
            int x = rs.x != 0 ? (rs.x > 0 ? 1 : -1) : 0;
            int y = rs.y != 0 ? (rs.y > 0 ? 1 : -1) : 0;
            int z = rs.z != 0 ? (rs.z > 0 ? 1 : -1) : 0;
            #endregion
            Vector3Int orientation = new Vector3Int(x, y, z);
            Vector3Int absOrientation = VectorMath.AbsVector(orientation);
            size = VectorMath.MultiplyVector(size, orientation);
            return (absOrientation, size);
        }
    }
}
