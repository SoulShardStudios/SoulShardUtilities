using UnityEngine;
namespace SoulShard.Utils
{
    [CreateAssetMenu(menuName = "SoulShardUtils/EntitySpawner2DProperties")]
    public class EntitySpawnerProps : ScriptableObject
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
        public Props[] props;
    }
}