using UnityEngine;
namespace SoulShard.Utils
{
    public static class GizmosHelp
    {
        public static void DrawRect(Rect rect, int PPU, Vector2 position)
        {
            int scale = PPU;
            Rect r = rect;
            Vector2 offset = -r.size / 2;

            Vector2 start = (r.position + offset);
            Vector2 up = (r.position + new Vector2(0, r.height) + offset);
            Vector2 top = (r.position + r.size + offset);
            Vector2 bottom = (r.position + new Vector2(r.width, 0) + offset);

            Vector2[] positions = new Vector2[] { start, up, top, bottom };
            for (int i = 0; i < 4; i++)
                positions[i] = positions[i] / scale + position;

            Gizmos.DrawLine(positions[0], positions[1]);
            Gizmos.DrawLine(positions[1], positions[2]);
            Gizmos.DrawLine(positions[2], positions[3]);
            Gizmos.DrawLine(positions[3], positions[0]);
        }
    }
}