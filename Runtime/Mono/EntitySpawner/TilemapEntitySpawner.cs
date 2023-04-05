using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

namespace SoulShard.Utils
{
    public class TilemapEntitySpawner : MonoBehaviour
    {
        [SerializeField]
        Tilemap _tilemap;

        [SerializeField]
        bool _spawnOnAwake;

        [SerializeField]
        TilemapEntitySpawnerProps _props;

        [SerializeField]
        Transform _spawnParent;

        void ApplySpawnConfigToTile(Rect rect, TileBase tile)
        {
            TilemapEntitySpawnerProps.SpawnConfig config = null;
            foreach (var c in _props.toSpawn)
            {
                if (c.chance <= Random.Range(0f, 1.1f))
                    continue;
                if (c.validTiles.Contains(tile))
                    config = c;
            }
            if (config == null)
                return;
            for (int i = 0; i < config.spawnPerTile.RandomBetween(); i++)
                Instantiate(
                    config.entities.RandomElement(),
                    rect.RandomInside(),
                    Quaternion.identity,
                    _spawnParent
                );
        }

        [ContextMenu("Spawn Entities")]
        void Spawn()
        {
            var grid = _tilemap.layoutGrid;
            for (var x = _tilemap.cellBounds.min.x; x < _tilemap.cellBounds.max.x; x++)
                for (var y = _tilemap.cellBounds.min.y; y < _tilemap.cellBounds.max.y; y++)
                {
                    var tilePos = new Vector3Int(x, y, 0);
                    var tile = _tilemap.GetTile(tilePos);
                    var worldRect = new Rect(grid.CellToWorld(tilePos), grid.cellSize);
                    ApplySpawnConfigToTile(worldRect, tile);
                }
        }

        void Awake()
        {
            if (_spawnOnAwake)
                Spawn();
        }
    }
}