using UnityEngine;
namespace SoulShard.Math
{
    /// <summary>
    /// a collection of general vector constants.
    /// </summary>
    public struct VectorConstants
    {
        #region Missing Vector Constants
        /// <summary>
        /// shorthand for writing Vector3(1, 0, 1)
        /// </summary>
        public static readonly Vector3 zx = new Vector3(1, 0, 1);
        /// <summary>
        /// shorthand for writing Vector3(0, 1, 1)
        /// </summary>
        public static readonly Vector3 zy = new Vector3(0, 1, 1);
        /// <summary>
        /// shorthand for writing Vector3(1, 1, 0)
        /// </summary>
        public static readonly Vector3 yx = new Vector3(1, 1, 0);
        /// <summary>
        /// shorthand for writing Vector3(-1, 0, -1)
        /// </summary>
        public static readonly Vector3 zxNegative = new Vector3(-1, 0, -1);
        /// <summary>
        /// shorthand for writing Vector3(0, -1, -1)
        /// </summary>
        public static readonly Vector3 zyNegative = new Vector3(0, -1, -1);
        /// <summary>
        /// shorthand for writing Vector3(-1, -1, 0)
        /// </summary>
        public static readonly Vector3 yxNegative = new Vector3(-1, -1, 0);
        /// <summary>
        /// shorthand for writing Vector3Int(1, 0, 1)
        /// </summary>
        public static readonly Vector3Int zxInt = new Vector3Int(1, 0, 1);
        /// <summary>
        /// shorthand for writing Vector3Int(0, 1, 1)
        /// </summary>
        public static readonly Vector3Int zyInt = new Vector3Int(0, 1, 1);
        /// <summary>
        /// shorthand for writing Vector3Int(1, 1, 0)
        /// </summary>
        public static readonly Vector3Int yxInt = new Vector3Int(1, 1, 0);
        /// <summary>
        /// shorthand for writing Vector3Int(-1, 0, -1)
        /// </summary>
        public static readonly Vector3Int zxNegativeInt = new Vector3Int(-1, 0, -1);
        /// <summary>
        /// shorthand for writing Vector3Int(0, -1, -1)
        /// </summary>
        public static readonly Vector3Int zyNegativeInt = new Vector3Int(0, -1, -1);
        /// <summary>
        /// shorthand for writing Vector3Int(-1, -1, 0)
        /// </summary>
        public static readonly Vector3Int yxNegativeInt = new Vector3Int(-1, -1, 0);
        #endregion
        #region Direction Collections
        /// <summary>
        /// cardinal directions in vector2 format
        /// </summary>
        public static Vector2[] CardianlsVf() => 
            new Vector2[4] { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right };
        /// <summary>
        /// cardinal directions in vector2Int format
        /// </summary>
        public static Vector2Int[] CardianlsVi() => 
            new Vector2Int[4] { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right };
        /// <summary>
        /// cardinal directions and zero in vector2 format
        /// </summary>
        public static Vector2[] CardianlsVfZero() => 
            new Vector2[5] { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.zero };
        /// <summary>
        /// cardinal directions and zero in vector2Int format
        /// </summary>
        public static Vector2Int[] CardianlsViZero() => 
            new Vector2Int[5] { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.zero };
        /// <summary>
        /// cardinal and diagonal directions and zero in vector2Int format
        /// </summary>
        public static Vector2Int[] CardinalsAndDiagonalsZeroVi() => 
            new Vector2Int[9] { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.zero, new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(1, 1) };
        /// <summary>
        /// cardinal and diagonal directions in vector2Int format
        /// </summary>
        public static Vector2Int[] CardinalsAndDiagonalsVi() => 
            new Vector2Int[8] { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right, new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(1, 1) };
        /// <summary>
        /// diagonal directions in vector2 format
        /// </summary>
        public static Vector2[] Diagonals() => 
            new Vector2[4] { new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f) };
        /// <summary>
        /// diagonal directions and zero in vector2 format
        /// </summary>
        public static Vector2[] DiagonalsZero() => 
            new Vector2[5] { new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f), Vector2.zero };
        /// <summary>
        /// cardinal and diagonal directions in vector2 format
        /// </summary>
        public static Vector2[] CardinalsAndDiagonalsVf() => 
            new Vector2[8] { Vector2.down, Vector2.up, Vector2.left, Vector2.right, new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f) };
        /// <summary>
        /// cardinal and diagonal directions and zero in vector2 format
        /// </summary>
        public static Vector2[] CardinalsAndDiagonalsZeroVf() => 
            new Vector2[9] { Vector2.down, Vector2.up, Vector2.left, Vector2.right, new Vector2(-0.75f, -0.75f), new Vector2(-0.75f, 0.75f), new Vector2(0.75f, -0.75f), new Vector2(0.75f, 0.75f), Vector2.zero };
        #endregion
        #region QuadInfo
        /// <summary>
        /// default 2d quad texture coordinates
        /// </summary>
        public static Vector2[] QuadUV() => 
            new Vector2[4] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
        /// <summary>
        /// default 2d quad verticies
        /// </summary>
        public static Vector3[] QuadVerts() => 
            new Vector3[4] { new Vector3(0, 1), new Vector3(1, 1), new Vector3(0, 0), new Vector3(1, 0) };
        /// <summary>
        /// default 2d quad indicies
        /// </summary>
        public static int[] QuadIndicies() => 
            new int[6] { 0, 1, 2, 2, 1, 3 };
        #endregion
    }
}