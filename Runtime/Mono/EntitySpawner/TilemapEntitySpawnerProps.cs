using UnityEngine.Tilemaps;
using UnityEngine;

namespace SoulShard.Utils
{
    [CreateAssetMenu(menuName = "SoulShardUtils/TilemapEntitySpawnerProperties")]
    public class TilemapEntitySpawnerProps : MonoBehaviour
    {
        [System.Serializable]
        public class SpawnConfig
        {
            public GameObject[] entities;
            public float chance;
            public Vector2Int spawnPerTile;
            public TileBase[] validTiles;
        }

        public SpawnConfig[] toSpawn;
    }
}
