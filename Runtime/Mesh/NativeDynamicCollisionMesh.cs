using UnityEngine;
using Unity.Collections;
using SoulShard.Math;
namespace SoulShard.Utils
{
    /// <summary>
    /// this is an object that allows collision mesh data to be pushed to it and then rendered into a final mesh.
    /// </summary>
    public struct NativeDynamicCollisionMesh : IDynamicCollisionMesh
    {
        #region Vars
        /// <summary>
        /// the verticies of the mesh
        /// </summary>
        public NativeList<Vector3> verticies;
        /// <summary>
        /// the indicies of the mesh
        /// </summary>
        public NativeList<int> indicies;
        /// <summary>
        /// the number of verticies in the previous set of added geometry
        /// </summary>
        public int previousVertsLength { get; private set; }
        /// <summary>
        /// indicates whether this object has allocated its memory or not
        /// </summary>
        public bool allocated { get; private set; }
        #endregion
        #region Management
        /// <summary>
        /// Initializes all native collections within the mesh.
        /// </summary>
        /// <param name="allocation">the allocation type for those collections</param>
        public void Init(Allocator allocation)
        {
            verticies = new NativeList<Vector3>(0, allocation);
            indicies = new NativeList<int>(0, allocation);
            previousVertsLength = 0;
            allocated = true;
        }
        /// <summary>
        /// disposes all native collections inside of the DynamicMesh. (MUST CALL AFTER YOU'VE GENERATED AND ARE DONE WITH THE MESH)
        /// </summary>
        public void Dispose()
        {
            if (!allocated)
                return;
            verticies.Dispose();
            indicies.Dispose();
            allocated = false;
        }
        /// <summary>
        /// clears the mesh data
        /// </summary>
        public void Clear()
        {
            verticies.Clear();
            indicies.Clear();
        }
        #endregion
        #region Adding
        /// <summary>
        /// add a piece of geometry to the mesh
        /// </summary>
        /// <param name="geometry">the geometry to add</param>
        public void AddGeometry((Vector3[], int[]) geometry) =>
            AddGeometry(geometry.Item1, geometry.Item2);
        /// <summary>
        /// add a piece of geometry to the mesh
        /// </summary>
        /// <param name="verticies">the verticies of the geometry</param>
        /// <param name="indicies">the indicies of the geometry</param>
        public void AddGeometry(Vector3[] verticies, int[] indicies)
        {
            indicies = MathUtility.AddToList(indicies, previousVertsLength);
            JobUtility.AddToNativeList(this.verticies, verticies);
            JobUtility.AddToNativeList(this.indicies, indicies);
            previousVertsLength = verticies.Length;
        }
        #endregion
        /// <summary>
        /// generates the mesh for use
        /// </summary>
        /// <returns>the generated mesh</returns>
        public Mesh Generate(string name = "")
        {
            Mesh Generated = new Mesh();
            Generated.name = name;
            Generated.vertices = verticies.ToArray();
            Generated.triangles = indicies.ToArray();
            return Generated;
        }
    }
}