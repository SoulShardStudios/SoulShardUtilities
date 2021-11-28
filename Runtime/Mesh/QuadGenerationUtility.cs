using UnityEngine;
using SoulShard.Math;
namespace SoulShard.Utils
{
    /// <summary>
    /// allowes for the simplified generation of quads for use in any mesh.
    /// </summary>
    public struct QuadGenerationUtility
    {
        #region Regular
        /// adds multiple scaled, sized, and positioned quads to a mesh
        /// </summary>
        /// <param name="mesh">the dynamic mesh to add these quads to</param>
        /// <param name="positions">the positions of the quads to add</param>
        /// <param name="scale">the scales of the quads to add</param>
        /// <param name="size">the sizes of the quads to add</param>
        public static void AddScaledQuadsToDynamicMesh(DynamicCollisionMesh mesh, Vector2[] positions, Vector3 scale, Vector3 size) =>
            AddScaledQuadsToDynamicMesh(mesh, positions, scale, CollectionUtility.GenerateNewArray(positions.Length, size));
        /// <summary>
        /// adds multiple scaled, sized, and positioned quads to a mesh
        /// </summary>
        /// <param name="mesh">the dynamic mesh to add these quads to</param>
        /// <param name="positions">the positions of the quads to add</param>
        /// <param name="scale">the scales of the quads to add</param>
        /// <param name="size">the sizes of the quads to add</param>
        public static void AddScaledQuadsToDynamicMesh(DynamicCollisionMesh mesh, Vector2[] positions, Vector3 scale, Vector3[] size)
        {
            for (int i = 0; i < positions.Length; i++)
                mesh.AddGeometry(GetQuad(positions[i], scale, size[i]));
        }
        #endregion
        /// <summary>
        /// gets a single scaled, positioned, and sized quad
        /// </summary>
        /// <param name="position">the position of the quad</param>
        /// <param name="scale">the scale of the quad</param>
        /// <param name="size">the size of the quad</param>
        /// <returns></returns>
        public static (Vector3[], int[]) GetQuad(Vector2 position, Vector3 scale, Vector3 size)
        {
            Vector3[] verts = GetQuadPositions(size);
            verts = VectorMath.TranslateVectorArray(verts, position);
            verts = VectorMath.MultiplyVectorArray(verts, scale);
            return (verts, VectorConstants.QuadIndicies);
        }
        static Vector3[] GetQuadPositions(Vector3 size)
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
            if (absOrientation == VectorConstants.zyInt)
                return new Vector3[] { new Vector3(0, 0, size.z), new Vector3(0, size.y, size.z), Vector3.zero, new Vector3(0, size.y, 0) };
            if (absOrientation == VectorConstants.zxInt)
                return new Vector3[] { new Vector3(0, 0, size.z), new Vector3(size.x, 0, size.z), Vector3.zero, new Vector3(size.x, 0, 0) };
            if (absOrientation == VectorConstants.yxInt)
                return new Vector3[] { new Vector3(0, size.y, 0), new Vector3(size.x, size.y, 0), Vector3.zero, new Vector3(size.x, 0, 0) };
            return null;
        }
    }
}