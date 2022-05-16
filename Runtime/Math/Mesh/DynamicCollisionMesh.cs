using UnityEngine;
using System.Collections.Generic;
using SoulShard.Math;

namespace SoulShard.Utils
{
    /// <summary>
    /// this is an object that allows collision mesh data to be pushed to it and then rendered into a final mesh.
    /// </summary>
    public class DynamicCollisionMesh
    {
        /// <summary>
        /// the verticies of the mesh
        /// </summary>
        public List<Vector3> verticies = new List<Vector3>(0);

        /// <summary>
        /// the indicies of the mesh
        /// </summary>
        public List<int> indicies = new List<int>(0);

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
            this.verticies.AddRange(verticies);
            this.indicies.AddRange(MathUtility.AddToList(indicies, this.verticies.Count));
        }

        /// <summary>
        /// clears the mesh data
        /// </summary>
        public void Clear()
        {
            verticies.Clear();
            indicies.Clear();
        }

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
