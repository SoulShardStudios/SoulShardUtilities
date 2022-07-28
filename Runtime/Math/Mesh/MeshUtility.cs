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

        /// <summary>
        /// Gets a random point on a mesh.
        /// </summary>
        /// <param name="mesh">The mesh to get a point for.</param>
        public static Vector3 GetRandomPointOnMesh(Mesh mesh)
        {
            int tris = (mesh.triangles.Length / 3) - 1;
            int triIndex = Mathf.RoundToInt(tris * Random.value);

            Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
            Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
            Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

            return GetRandomPointOnTriangle(a, b, c);
        }

        /// <summary>
        /// Gets a random point on a triangle.
        /// </summary>
        /// <param name="a">The first point of the triangle.</param>
        /// <param name="b">The second point of the triangle</param>
        /// <param name="c">The last point of the triangle</param>
        public static Vector3 GetRandomPointOnTriangle(Vector3 a, Vector3 b, Vector3 c)
        {
            float r = Random.value;
            float s = Random.value;

            if (r + s >= 1)
            {
                r = 1 - r;
                s = 1 - s;
            }

            Vector3 pointOnMesh = a + r * (b - a) + s * (c - a);
            return pointOnMesh;
        }
    }
}
