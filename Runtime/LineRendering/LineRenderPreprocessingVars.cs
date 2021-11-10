using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// a small struct for storing the parameters needed to compute the line algorithm
    /// </summary>
    public struct LineRenderVars
    {
        // input positions
        public Vector2Int pos0;
        public Vector2Int pos1;
        // line parameters
        public bool steep;
        public int error;
        public int ystep;
        public int y;
        public int dx;
        public int dy;
    }
}