using UnityEngine;
using Unity.Collections;
namespace SoulShard.Utils
{
    /// <summary>
    /// this is an object that allows collision mesh data to be pushed to it and then rendered into a final mesh.
    /// </summary>
    public struct NativeDynamicCollisionMesh: IDynamicCollisionMesh
    {
        public NativeList<Vector3> verticies;
        public NativeList<int> indicies;
        int previousVertsLength;
        public void Dispose()
        {
            verticies.Dispose();
            indicies.Dispose();
        }
        public void Init()
        {
            verticies = new NativeList<Vector3>();
            indicies = new NativeList<int>();
            previousVertsLength = 0;
        }
        public void AddGeometry((Vector3[], int[]) geometry) =>
            AddGeometry(geometry.Item1, geometry.Item2);
        public void AddGeometry(Vector3[] verticies, int[] indicies)
        {
            JobUtility.AddToNativeList(this.verticies, verticies);
            JobUtility.AddToNativeList(this.indicies, MathUtility.AddToList(indicies, previousVertsLength));
            previousVertsLength = verticies.Length;
        }
        public void Clear()
        {
            verticies.Clear();
            indicies.Clear();
        }
        public Mesh Generate()
        {
            Mesh Generated = new Mesh();
            Generated.vertices = verticies.ToArray();
            Generated.triangles = indicies.ToArray();
            return Generated;
        }
    }
}