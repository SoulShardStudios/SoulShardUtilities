using UnityEngine.Tilemaps;
using UnityEngine;

namespace SoulShard.Utils
{
    [CreateAssetMenu(menuName = "SoulShardUtils/TilemapEntitySpawnerProperties")]
    public class TilemapEntitySpawnerProps : ScriptableObject
    {
        [Tooltip(tooltip: "The entities that can be spawned on this tile")]
        public GameObject[] entities;

        [Tooltip(tooltip: "The chance that this spawn config will be used")]
        public float chance;

        [Tooltip(tooltip: "How many things can be spawned on this tile")]
        public Vector2Int spawnPerTile;

        [Tooltip(tooltip: "Which tiles are valid to spawn this entity on")]
        public TileBase[] validTiles;

        [Tooltip(
            tooltip: "The minimum distance in tiles between spawned members of the same configuration."
        )]
        public uint tileGap;

        [Tooltip(
            tooltip: "The minimum gap distance between where this spawn config will be applied and a forbidden tilemap"
        )]
        public uint tileGapToForbiddenTilemap;

        [Tooltip(
            tooltip: "A percentage of the tile's size. The amount of padding to add so the entities spawn closer to the center."
        )]
        public float tileEdgePad;
    }
}
