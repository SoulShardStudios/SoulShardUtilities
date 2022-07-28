using UnityEngine;
using UnityEditor;

namespace SoulShard.Utils
{
    /// <summary>
    /// Fast automation for randomly spawning entities over an area in a natural way.
    /// </summary>
    public class EntitySpawner2D : MonoBehaviour
    {
        /// <summary>
        /// The properties for each stage of spawning.
        /// </summary>
        [System.Serializable]
        public class Props
        {
            /// <summary>
            /// The collection of entities that could be spawned (Selected randomly)
            /// </summary>
            public GameObject[] entities;

            /// <summary>
            /// The number of attempts to spawn an entity that should be performed.
            /// </summary>
            public Vector2 spawnAmount;

            /// <summary>
            /// The minimum distance away from other entities inside of the transform parent that new entities must be.
            /// </summary>
            public float minimumDistance;
        }

        /// <summary>
        /// The list of properties of each stage of spawning.
        /// Goes from the first element to the last and spawns using those parameters in order.
        /// </summary>
        [SerializeField]
        Props[] _props;

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
        bool _spawnOnEnable;

        void OnEnable()
        {
            if (_spawnOnEnable)
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
            foreach (Props p in _props)
                SpawnEntities(p);
        }

        void SpawnEntities(Props props)
        {
            if (props.entities.Length == 0)
            {
                Debug.LogError("No entities were provided to the spawner.");
                return;
            }
            var mesh = MeshUtility.ConvexPointsToMesh2D(spawnArea, 0);
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
                GameObject g = (GameObject)PrefabUtility.InstantiatePrefab(
                    entity,
                    _spawnParent.transform
                );
                if (g == null)
                    g = Instantiate(entity, _spawnParent.transform);
                g.transform.position = pos;
            }
        }
    }
}
