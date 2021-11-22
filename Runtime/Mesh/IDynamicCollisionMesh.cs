using UnityEngine;
namespace SoulShard.Utils
{
    public interface IDynamicCollisionMesh
    {
        public void Clear();
        public Mesh Generate(string name);
        public void AddGeometry((Vector3[], int[]) geometry);
        public void AddGeometry(Vector3[] verticies, int[] indicies);
    }
}