using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SoulShard.Utils
{
    public class ExtendedMono : MonoBehaviour
    {
        /// <summary>
        /// If we're in the unity editor, and the entity is a prefab, instantiate the prefab with a prefab reference.
        /// Otherwise, instantiate normally. Because of the restrictions on instantiate, this function must live inside a monobehavior.
        /// </summary>
        /// <param name="object">The object to spawn</param>
        /// <param name="parent">The transform parent of this object.</param>
        /// <returns>The spawned object.</returns>
        public static GameObject InstantiateWithReference(GameObject @object, Transform parent)
        {
#if UNITY_EDITOR
            GameObject g = (GameObject)PrefabUtility.InstantiatePrefab(@object, parent);
            if (g == null)
                g = Instantiate(@object, parent);
#endif
#if !UNITY_EDITOR
            GameObject g = Instantiate(@object, parent.transform);
#endif
            return g;
        }
    }
}
