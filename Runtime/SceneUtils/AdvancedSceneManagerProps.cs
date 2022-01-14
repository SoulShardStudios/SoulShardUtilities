using UnityEngine;
namespace SoulShard.Utils
{
    [CreateAssetMenu(menuName = "SoulShardUtils/AdvancedSceneManagerProperties")]
    public class AdvancedSceneManagerProps : ScriptableObject
    {
        /// <summary>
        /// scenes loaded when this object is enabled
        /// </summary>
        public string[] loadOnEnable;
        /// <summary>
        /// scenes that will always be loaded
        /// </summary>
        public string[] loadAlways;
        /// <summary>
        /// a collection of scene maps that will determine scene dependencies
        /// </summary>
        public SceneToScene[] loadWhileOtherIsLoaded;
    }
}