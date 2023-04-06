using UnityEngine.Tilemaps;
using UnityEngine;

namespace SoulShard.Utils
{
    [CreateAssetMenu(menuName = "SoulShardUtils/TilemapEntitySpawnerProperties")]
    public class TilemapEntitySpawnerProps : ScriptableObject
    {
        public GameObject[] entities;
        public float chance;
        public Vector2Int spawnPerTile;
        public TileBase[] validTiles;

        [Tooltip(
            tooltip: "The minimum distance in tiles between spawned members of the same configuration."
        )]
        public uint tileGap;
    }
}
