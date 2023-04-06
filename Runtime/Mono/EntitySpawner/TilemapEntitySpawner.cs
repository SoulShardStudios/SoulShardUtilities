using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections.Generic;
using SoulShard.Math;

namespace SoulShard.Utils
{
    public class TilemapEntitySpawner : ExtendedMono
    {
        [SerializeField]
        Tilemap _tilemap;

        [SerializeField]
        bool _spawnOnAwake;

        [SerializeField]
        TilemapEntitySpawnerProps[] _props;

        [SerializeField]
        Transform _spawnParent;

        Dictionary<uint, Vector2Int[]> _spawnDeltaCache = new Dictionary<uint, Vector2Int[]>();

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
                foreach (Vector3Int v in deltas)
                    newDeltas.UnionWith(
                        VectorMath.TranslateVectorArray(
                            VectorConstants.CardinalsAndDiagonalsVi(),
                            (Vector2Int)v
                        )
                    );
                deltas.UnionWith(newDeltas);
                newDeltas.Clear();
            }
            return _spawnDeltaCache[size] = deltas.ToArray();
        }

        [ContextMenu("Spawn Entities")]
        void Spawn()
        {
            TilemapEntitySpawnerProps[,] chosenConfigs = new TilemapEntitySpawnerProps[
                _tilemap.cellBounds.size.x,
                _tilemap.cellBounds.size.y
            ];

            Vector3Int tilemapMin = _tilemap.cellBounds.min;

            void ApplySpawnConfigToTile(Rect rect, TileBase tile, Vector3Int tilePos)
            {
                Vector2Int chosenConfigsIdx = (Vector2Int)(tilePos - tilemapMin);
                TilemapEntitySpawnerProps config = null;
                foreach (var c in _props)
                {
                    if (c.chance <= Random.Range(0f, 1.1f))
                        continue;
                    if (!c.validTiles.Contains(tile))
                        continue;
                    if (c.tileGap == 0)
                    {
                        config = c;
                        break;
                    }
                    foreach (var v in GetTileGapDeltas(c.tileGap))
                    {
                        var pos = v + chosenConfigsIdx;
                        TilemapEntitySpawnerProps configAtPos;
                        try
                        {
                            configAtPos = chosenConfigs[pos.x, pos.y];
                        }
                        catch
                        {
                            continue;
                        }
                        if (configAtPos == c)
                            return;
                    }
                    config = c;
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

            var grid = _tilemap.layoutGrid;
            for (var x = _tilemap.cellBounds.min.x; x < _tilemap.cellBounds.max.x; x++)
                for (var y = _tilemap.cellBounds.min.y; y < _tilemap.cellBounds.max.y; y++)
                {
                    var tilePos = new Vector3Int(x, y, 0);
                    var tile = _tilemap.GetTile(tilePos);
                    var worldRect = new Rect(grid.CellToWorld(tilePos), grid.cellSize);
                    ApplySpawnConfigToTile(worldRect, tile, tilePos);
                }
        }

        void Awake()
        {
            if (_spawnOnAwake)
                Spawn();
        }
    }
}
