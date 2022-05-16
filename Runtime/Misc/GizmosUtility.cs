using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// a utility for drawing specifc things easier with gizmos
    /// </summary>
    public static class GizmosUtility
    {
        /// <summary>
        /// draws a rect with some given parameters with unity gizmos
        /// </summary>
        /// <param name="rect">the rect to draw</param>
        /// <param name="PPU">the pixels per unit of the rect</param>
        /// <param name="position">the position of the rect</param>
        public static void DrawRect(Rect rect, int PPU, Vector2 position)
        {
            int scale = PPU;
            Rect r = rect;
            Vector2 start = r.position;
            Vector2 up = r.position + new Vector2(0, r.height);
            Vector2 top = r.position + r.size;
            Vector2 bottom = r.position + new Vector2(r.width, 0);

            Vector2[] positions = new Vector2[] { start, up, top, bottom };
            for (int i = 0; i < 4; i++)
                positions[i] = positions[i] / scale + position;

            Gizmos.DrawLine(positions[0], positions[1]);
            Gizmos.DrawLine(positions[1], positions[2]);
            Gizmos.DrawLine(positions[2], positions[3]);
            Gizmos.DrawLine(positions[3], positions[0]);
        }

        /// <summary>
        /// draws a rect with some given parameters with unity gizmos
        /// </summary>
        /// <param name="rect">the rect to draw</param>
        public static void DrawRect(Rect rect)
        {
            Vector2 start = rect.position;
            Vector2 up = rect.position + new Vector2(0, rect.height);
            Vector2 top = rect.position + rect.size;
            Vector2 bottom = rect.position + new Vector2(rect.width, 0);
            Vector2[] positions = new Vector2[] { start, up, top, bottom };
            Gizmos.DrawLine(positions[0], positions[1]);
            Gizmos.DrawLine(positions[1], positions[2]);
            Gizmos.DrawLine(positions[2], positions[3]);
            Gizmos.DrawLine(positions[3], positions[0]);
        }
    }
}
