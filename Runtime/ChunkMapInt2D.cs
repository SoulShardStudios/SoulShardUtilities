using UnityEngine;
using System.Collections.Generic;
namespace SoulShard.Utils
{
    [System.Serializable]
    public class ChunkMapInt2D<T> where T : MonoBehaviour
    {
        [HideInInspector] public Dictionary<Vector2Int, T> chunks = new Dictionary<Vector2Int, T>();
        [HideInInspector] public int PPU;
        public uint chunkSize;
        public bool drawChunkBorders;
        public Color chunkBorderColor;
        public Vector2Int chunkSizeV2I { get => new Vector2Int((int)chunkSize, (int)chunkSize); }
        public Vector2Int GetChunkPosFromWorldPos(Vector2Int position) => VectorMath.RoundVector2((Vector2)position / chunkSizeV2I);
        public Vector2Int GetPositionWithinChunk(Vector2Int position) => VectorMath.ModVector2(position, (int)chunkSize) + ((chunkSizeV2I / 2));
        public ChunkMapInt2D(Color chunkBorderColor, uint chunkSize = 1, bool drawChunkBorders = true)
        {
            this.chunkSize = chunkSize;
            this.chunkBorderColor = chunkBorderColor;
            this.drawChunkBorders = drawChunkBorders;
        }
        public ChunkMapInt2D(uint chunkSize = 1, bool drawChunkBorders = true)
        {
            this.chunkSize = chunkSize;
            chunkBorderColor = Color.white;
            this.drawChunkBorders = drawChunkBorders;
        }
        // draws boundaries on all the chunks for debugging
        public void DrawGizmoBorders()
        {
            Gizmos.color = chunkBorderColor;
            if (!drawChunkBorders)
                return;
            foreach (KeyValuePair<Vector2Int, T> k in chunks)
                GizmosHelp.DrawRect(new Rect(k.Value.transform.position, chunkSizeV2I), PPU, k.Value.gameObject.transform.position);
        }
        // gets a specific chunk. this is needed to make sure my idiot self doesn't cause a keynotfoundexception when acessing the dict
        public T GetChunk(Vector2Int position)
        {
            try { return chunks[position]; }
            catch (KeyNotFoundException) { return null; }
        }
    }
}