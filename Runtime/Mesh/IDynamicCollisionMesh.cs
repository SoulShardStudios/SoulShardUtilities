using UnityEngine;
namespace SoulShard.Utils
{
    interface IDynamicCollisionMesh
    {
        public void Clear();
        public Mesh Generate();
        public void AddGeometry((Vector3[], int[]) geometry);
        public void AddGeometry(Vector3[] verticies, int[] indicies);
    }
}