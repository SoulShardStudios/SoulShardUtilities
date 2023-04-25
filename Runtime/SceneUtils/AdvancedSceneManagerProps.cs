using UnityEngine;

namespace SoulShard.Utils
{
    [CreateAssetMenu(menuName = "SoulShardUtils/AdvancedSceneManagerProperties")]
    public class AdvancedSceneManagerProps : ScriptableObject
    {
        [Tooltip(tooltip: "Scenes loaded when this object is enabled")]
        public string[] loadOnEnable;

        [Tooltip(tooltip: "Scenes that will always be loaded")]
        public string[] loadAlways;

        [Tooltip(tooltip: "A collection of scene maps that will determine scene dependencies")]
        public SceneToScene[] loadWhileOtherIsLoaded;

        [Tooltip(tooltip: "The starting active scene")]
        public string startingActiveScene;
    }
}
