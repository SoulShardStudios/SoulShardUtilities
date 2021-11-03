using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using SoulShard.Utils;
// renders a rasterized line according to bresenhams line algorithm
public struct LineRenderer
{

    //source: http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html
    public static IEnumerable<Vector2Int> InterpolateLine(Vector2Int pos0, Vector2Int pos1)
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
        for (int x = pos0.x; x <= pos1.x; x++)
        {
            yield return new Vector2Int(steep ? y : x, steep ? x : y);
            error = error - dy;
            if (error < 0)
            {
                y += ystep;
                error += dx;
            }
        }
        yield break;
    }
    public static Vector2Int[] InterpolateLine(Vector2Int pos0, Vector2Int pos1, Vector2Int[] shape) => VectorMath.TranslatePositionsToArray(InterpolateLine(pos0, pos1).ToArray(), shape);
}