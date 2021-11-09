using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// a collection of general vector constants.
    /// </summary>
    public struct VectorConstants
    {
        #region Direction Collections
        /// <summary>
        /// cardinal directions in vector2 format
        /// </summary>
        public static readonly Vector2[] CardianlsVf = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right };
        /// <summary>
        /// cardinal directions in vector2Int format
        /// </summary>
        public static readonly Vector2Int[] CardianlsVi = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right };
        /// <summary>
        /// cardinal directions and zero in vector2 format
        /// </summary>
        public static readonly Vector2[] CardianlsVfZero = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.zero };
        /// <summary>
        /// cardinal directions and zero in vector2Int format
        /// </summary>
        public static readonly Vector2Int[] CardianlsViZero = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.zero };
        /// <summary>
        /// cardinal and diagonal directions and zero in vector2Int format
        /// </summary>
        public static readonly Vector2Int[] CardinalsAndDiagonalsZeroVi = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.zero, new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(1, 1) };
        /// <summary>
        /// cardinal and diagonal directions in vector2Int format
        /// </summary>
        public static readonly Vector2Int[] CardinalsAndDiagonalsVi = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(1, 1) };
        /// <summary>
        /// diagonal directions in vector2 format
        /// </summary>
        public static readonly Vector2[] Diagonals = { new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f) };
        /// <summary>
        /// diagonal directions and zero in vector2 format
        /// </summary>
        public static readonly Vector2[] DiagonalsZero = { new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f), Vector2.zero };
        /// <summary>
        /// cardinal and diagonal directions in vector2 format
        /// </summary>
        public static readonly Vector2[] CardinalsAndDiagonalsVf = { Vector2.down, Vector2.up, Vector2.left, Vector2.right, new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f) };
        /// <summary>
        /// cardinal and diagonal directions and zero in vector2 format
        /// </summary>
        public static readonly Vector2[] CardinalsAndDiagonalsZeroVf = { Vector2.down, Vector2.up, Vector2.left, Vector2.right, new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f), Vector2.zero };
        #endregion
        #region QuadInfo
        /// <summary>
        /// default 2d quad texture coordinates
        /// </summary>
        public static readonly Vector2[] QuadUV = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
        /// <summary>
        /// default 2d quad verticies
        /// </summary>
        public static readonly Vector3[] QuadVerts = { new Vector3(0, 1), new Vector3(1, 1), new Vector3(0, 0), new Vector3(1, 0) };
        /// <summary>
        /// default 2d quad indicies
        /// </summary>
        public static readonly int[] QuadIndicies = { 0, 1, 2, 2, 1, 3 };
        #endregion
    }
}