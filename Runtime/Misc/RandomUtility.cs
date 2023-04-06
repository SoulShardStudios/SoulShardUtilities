using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace SoulShard.Utils
{
    public static class RandomUtility
    {
        public static Vector2 RandomInside(this Rect rect) =>
            new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));

        public static float RandomBetween(this Vector2 vector) => Random.Range(vector.x, vector.y);

        public static int RandomBetween(this Vector2Int vector) => Random.Range(vector.x, vector.y);

        public static T RandomElement<T>(this IEnumerator<T> enumerator)
        {
            var arr = enumerator.GetEnumerable().ToArray();
            return arr[Random.Range(0, arr.Length)];
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerator)
        {
            var arr = enumerator.ToArray();
            return arr[Random.Range(0, arr.Length)];
        }

        public static T RandomElement<T>(this List<T> list) => list[Random.Range(0, list.Count())];

        public static T RandomElement<T>(this T[] array) => array[Random.Range(0, array.Count())];

        public static Vector3 RandomPoint(this Mesh mesh)
        {
            int tris = (mesh.triangles.Length / 3) - 1;
            int triIndex = Mathf.RoundToInt(tris * Random.value);

            Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
            Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
            Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

            return GetRandomPointOnTriangle(a, b, c);
        }

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
