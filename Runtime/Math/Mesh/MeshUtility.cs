using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// Utility functions related to meshes.
    /// </summary>
    public static class MeshUtility
    {
        /// <summary>
        /// Converts a list of convex points into a 2d mesh.
        /// </summary>
        /// <param name="points">The points to convert.</param>
        /// <param name="z">The z value of the points to convert.</param>
        public static Mesh ConvexPointsToMesh2D(Vector2[] points, float z = 0)
        {
            var mesh = new Mesh();
            var pts = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
                pts[i] = new Vector3(points[i].x, points[i].y, z);

            mesh.vertices = pts;
            var triCount = points.Length - 2;
            int[] tris = new int[triCount * 3];
            for (int i = 0; i < triCount; i++)
            {
                tris[i * 3] = 0;
                tris[i * 3 + 1] = i + 1;
                tris[i * 3 + 2] = i + 2;
            }

            mesh.triangles = tris;
            return mesh;
        }
    }
}
