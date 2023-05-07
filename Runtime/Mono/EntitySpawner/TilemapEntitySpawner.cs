using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections.Generic;
using SoulShard.Math;

namespace SoulShard.Utils
{
    public class TilemapEntitySpawner : ExtendedMono
    {
        [Tooltip(tooltip: "The tilemap to spawn entities on")]
        [SerializeField]
        Tilemap _tilemap;

        [Tooltip(tooltip: "The tilemap to avoid placing entities on")]
        [SerializeField]
        Tilemap[] _forbiddenTilemaps;

        [Tooltip(tooltip: "Whether the entities should spawn when the game starts")]
        [SerializeField]
        bool _spawnOnAwake;

        [Tooltip(tooltip: "The spawn configurations to spawn")]
        [SerializeField]
        TilemapEntitySpawnerProps[] _props;

        [Tooltip(tooltip: "The transform parent of the spawned entities")]
        [SerializeField]
        Transform _spawnParent;

        Dictionary<uint, Vector2Int[]> _spawnDeltaCache = new Dictionary<uint, Vector2Int[]>();

        Vector3Int _tilemapMin => _tilemap.cellBounds.min;

        Vector2Int[] GetTileGapDeltas(uint size)
        {
            if (_spawnDeltaCache.ContainsKey(size))
                return _spawnDeltaCache[size];
            HashSet<Vector2Int> deltas = new HashSet<Vector2Int>(
                new Vector2Int[1] { new Vector2Int(0, 0) }
            );
            HashSet<Vector2Int> newDeltas = new HashSet<Vector2Int>();
            for (int i = 0; i < size; i++)
            {
                foreach (Vector3Int delta in deltas)
                    newDeltas.UnionWith(
                        VectorConstants
                            .CardinalsAndDiagonalsVi()
                            .Select((x) => x + (Vector2Int)delta)
                    );
                deltas.UnionWith(newDeltas);
                newDeltas.Clear();
            }
            return _spawnDeltaCache[size] = deltas.ToArray();
        }

        bool OverlapsForbiddenTilemap(uint distance, Vector3Int tilePos)
        {
            foreach (var delta in GetTileGapDeltas(distance))
            {
                var pos = delta + (Vector2Int)tilePos;
                foreach (var t in _forbiddenTilemaps)
                    if (t.GetTile((Vector3Int)pos) != null)
                        return true;
            }
            return false;
        }

        bool OverlapsConfig(
            TilemapEntitySpawnerProps[,] chosenConfigs,
            TilemapEntitySpawnerProps configToCheck,
            Vector2Int chosenConfigsIdx
        )
        {
            if (configToCheck.tileGap == 0)
                return false;
            foreach (var delta in GetTileGapDeltas(configToCheck.tileGap))
            {
                var pos = delta + chosenConfigsIdx;
                TilemapEntitySpawnerProps configAtPos;
                try
                {
                    configAtPos = chosenConfigs[pos.x, pos.y];
                }
                catch
                {
                    continue;
                }
                if (configAtPos == configToCheck)
                    return true;
            }
            return false;
        }

        void ApplySpawnConfigToTile(
            Rect rect,
            TileBase tile,
            Vector3Int tilePos,
            TilemapEntitySpawnerProps[,] chosenConfigs
        )
        {
            Vector2Int chosenConfigsIdx = (Vector2Int)(tilePos - _tilemapMin);
            TilemapEntitySpawnerProps config = null;
            foreach (var configToCheck in _props)
            {
                if (configToCheck.chance <= Random.Range(0f, 1.1f))
                    continue;
                if (!configToCheck.validTiles.Contains(tile))
                    continue;
                if (OverlapsConfig(chosenConfigs, configToCheck, chosenConfigsIdx))
                    continue;
                if (OverlapsForbiddenTilemap(configToCheck.tileGapToForbiddenTilemap, tilePos))
                    continue;
                config = configToCheck;
                break;
            }
            if (config == null)
                return;
            chosenConfigs[chosenConfigsIdx.x, chosenConfigsIdx.y] = config;
            for (int i = 0; i < config.spawnPerTile.RandomBetween(); i++)
                InstantiateWithReference(
                    config.entities.RandomElement(),
                    _spawnParent
                ).transform.position = rect.RandomInside();
        }

        [ContextMenu("Spawn Entities")]
        void Spawn()
        {
            TilemapEntitySpawnerProps[,] chosenConfigs = new TilemapEntitySpawnerProps[
                _tilemap.cellBounds.size.x,
                _tilemap.cellBounds.size.y
            ];

            var grid = _tilemap.layoutGrid;
            for (var x = _tilemap.cellBounds.min.x; x < _tilemap.cellBounds.max.x; x++)
                for (var y = _tilemap.cellBounds.min.y; y < _tilemap.cellBounds.max.y; y++)
                {
                    var tilePos = new Vector3Int(x, y, 0);
                    var tile = _tilemap.GetTile(tilePos);
                    var worldRect = new Rect(grid.CellToWorld(tilePos), grid.cellSize);
                    ApplySpawnConfigToTile(worldRect, tile, tilePos, chosenConfigs);
                }
        }

        void Awake()
        {
            if (_spawnOnAwake)
                Spawn();
        }
    }
}
