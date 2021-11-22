using UnityEngine;
namespace SoulShard.Utils
{
    public interface IDynamicCollisionMesh
    {
        public void Clear();
        public Mesh Generate();
        public void AddGeometry((Vector3[], int[]) geometry);
        public void AddGeometry(Vector3[] verticies, int[] indicies);
    }
}