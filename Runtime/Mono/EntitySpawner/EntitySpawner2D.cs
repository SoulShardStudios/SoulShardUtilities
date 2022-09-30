using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using SoulShard.Math;

namespace SoulShard.Utils
{
    /// <summary>
    /// Fast automation for randomly spawning entities over an area in a natural way.
    /// </summary>
    public class EntitySpawner2D : MonoBehaviour
    {
        /// <summary>
        /// The props for what entities showld be spawned in what order and what density.
        /// </summary>
        [SerializeField]
        EntitySpawnerProps props;

        /// <summary>
        /// The transform parent to spawn the entites to.
        /// </summary>
        [SerializeField]
        Transform _spawnParent;

        /// <summary>
        /// The polygon in which to spawn the entities
        /// </summary>
        public Vector2[] spawnArea;

        /// <summary>
        /// Should the script spawn entities on enable?
        /// </summary>
        [SerializeField]
        bool _spawnOnAwake;

        void Awake()
        {
            if (_spawnOnAwake)
                SpawnAllProps();
        }

        bool PointIsValid(Vector2 point, float minDist)
        {
            for (int t = 0; t < _spawnParent.childCount; t++)
                if (Vector2.Distance(_spawnParent.GetChild(t).position, point) < minDist)
                    return false;
            return true;
        }

        /// <summary>
        /// With the parameters on this monobehavior, spawn some entities.
        /// </summary>
        public void SpawnAllProps()
        {
            foreach (var p in props.props)
                SpawnEntities(p);
        }

        void SpawnEntities(EntitySpawnerProps.Props props)
        {
            if (props.entities.Length == 0)
            {
                Debug.LogError("No entities were provided to the spawner.");
                return;
            }
            var mesh = MeshUtility.ConvexPointsToMesh2D(
                VectorMath.TranslateVectorArray(spawnArea, transform.position),
                0
            );
            for (
                int i = 0;
                i < Mathf.Ceil(Random.Range(props.spawnAmount.x, props.spawnAmount.y));
                i++
            )
            {
                var pos = MeshUtility.GetRandomPointOnMesh(mesh);
                if (!PointIsValid(pos, props.minimumDistance))
                    continue;
                GameObject entity = props.entities[Random.Range(0, props.entities.Length)];

                SpawnEntity(entity).transform.position = pos;
            }
        }

        GameObject SpawnEntity(GameObject entity)
        {
#if UNITY_EDITOR
            GameObject g = (GameObject)PrefabUtility.InstantiatePrefab(
                entity,
                _spawnParent.transform
            );
            if (g == null)
                g = Instantiate(entity, _spawnParent.transform);
#endif
#if !UNITY_EDITOR
            GameObject g = Instantiate(entity, _spawnParent.transform);
#endif
            return g;
        }
    }
}
