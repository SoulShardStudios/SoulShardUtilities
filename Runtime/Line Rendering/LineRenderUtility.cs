using UnityEngine;
using System.Linq;
using SoulShard.Math;
namespace SoulShard.Utils
{
    /// <summary>
    /// struct responsible for containing the code that renders a rasterized line according to bresenhams line algorithm
    /// Source for the algorithm: http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html
    /// </summary>
    public partial struct LineRenderUtility
    {
        /// <summary>
        /// the line renderer preprocessing used to generate the parameters for computing the line
        /// </summary>
        /// <param name="pos0">beginning position for a line</param>
        /// <param name="pos1">ending position for the line</param>
        /// <returns></returns>
        public static LineRenderVars LineRenderPreProcessing(Vector2Int pos0, Vector2Int pos1)
        {
            bool steep = Mathf.Abs(pos1.y - pos0.y) > Mathf.Abs(pos1.x - pos0.x);
            if (steep)
            {
                int t;
                t = pos0.x; // swap startPos.x and startPos.y
                pos0.x = pos0.y;
                pos0.y = t;
                t = pos1.x; // swap endPos.x and endPos.y
                pos1.x = pos1.y;
                pos1.y = t;
            }
            if (pos0.x > pos1.x)
            {
                int t;
                t = pos0.x; // swap startPos.x and endPos.x
                pos0.x = pos1.x;
                pos1.x = t;
                t = pos0.y; // swap startPos.y and endPos.y
                pos0.y = pos1.y;
                pos1.y = t;
            }
            int dx = pos1.x - pos0.x;
            int dy = Mathf.Abs(pos1.y - pos0.y);
            int error = dx / 2;
            int ystep = (pos0.y < pos1.y) ? 1 : -1;
            int y = pos0.y;
            return new LineRenderVars()
            {
                // render params
                steep = steep,
                error = error,
                ystep = ystep,
                y = y,
                dx = dx,
                dy = dy,
                // input positions
                pos0 = pos0,
                pos1 = pos1,
            };
        }
        /// <summary>
        /// interpolates a line with a given shape copied to every positon
        /// </summary>
        /// <param name="pos0">beginning position for the line</param>
        /// <param name="pos1">ending position for the line</param>
        /// <param name="shape">the shape that is copied over the line</param>
        /// <returns>a rasterized line</returns>
        public static Vector2Int[] InterpolateLineWithShapeApplied(Vector2Int pos0, Vector2Int pos1, Vector2Int[] shape) => 
            VectorMath.TranslateVectorsToArray(InterpolateLine(pos0, pos1).ToArray(), shape);
    }
}