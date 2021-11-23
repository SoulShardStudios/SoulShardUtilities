using UnityEngine;
using SoulShard.Math;
namespace SoulShard.Utils
{
    /// <summary>
    /// allowes for the simplified generation of quads for use in any mesh.
    /// </summary>
    public struct QuadGenerationUtility
    {
        #region Multiple Quad Operations
        // the reason the IDynamicCollisionMesh interface wasn't used to cut down on code size is
        // because it would break with the Native version due to reference errors. should still be useful elsewhere
        #region Native
        /// <summary>
        /// adds multiple scaled, sized, and positioned quads to a mesh
        /// </summary>
        /// <param name="mesh">the dynamic mesh to add these quads to</param>
        /// <param name="positions">the positions of the quads to add</param>
        /// <param name="scale">the scales of the quads to add</param>
        /// <param name="size">the sizes of the quads to add</param>
        public static void AddScaledQuadsToDynamicMesh(ref NativeDynamicCollisionMesh mesh, Vector2[] positions, Vector3 scale, Vector3 size) =>
            AddScaledQuadsToDynamicMesh(ref mesh, positions, scale, CollectionUtility.GenerateNewArray(positions.Length, size));
        /// <summary>
        /// adds multiple scaled, sized, and positioned quads to a mesh
        /// </summary>
        /// <param name="mesh">the dynamic mesh to add these quads to</param>
        /// <param name="positions">the positions of the quads to add</param>
        /// <param name="scale">the scales of the quads to add</param>
        /// <param name="size">the sizes of the quads to add</param>
        public static void AddScaledQuadsToDynamicMesh(ref NativeDynamicCollisionMesh mesh, Vector2[] positions, Vector3 scale, Vector3[] size)
        {
            for (int i = 0; i < positions.Length; i++)
                mesh.AddGeometry(GetQuad(positions[i], scale, size[i]));
        }
        #endregion
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
            Vector3Int orientation = VectorMath.VectorOperation(VectorMath.RoundVector(size), (int a) => a != 0 ? (a > 0 ? 1 : -1) : 0);
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