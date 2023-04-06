using UnityEngine;
using SoulShard.Math;

namespace SoulShard.Utils
{
    /// <summary>
    /// Fast automation for randomly spawning entities over an area in a natural way.
    /// </summary>
    public class EntitySpawner2D : ExtendedMono
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
                i < Mathf.Ceil(props.spawnAmount.RandomBetween());
                i++
            )
            {
                var pos = mesh.RandomPoint();
                if (!PointIsValid(pos, props.minimumDistance))
                    continue;
                GameObject entity = props.entities[Random.Range(0, props.entities.Length)];
                InstantiateWithReference(entity, _spawnParent).transform.position = pos;
            }
        }
    }
}
