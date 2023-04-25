using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// Exposes some AdvancedSceneManager APIs for monobehaviors.
    /// </summary>
    public class AdvancedSceneManagerMono : MonoBehaviour
    {
        public void Load(string name) => AdvancedSceneManager.Load(name);

        public void Unload(string name) => AdvancedSceneManager.Unload(name);

        public void UnloadCurrent() => AdvancedSceneManager.Unload(gameObject.scene.name);

        public void ActiveSceneTransition(string name) =>
            AdvancedSceneManager.ActiveSceneTransition(name);
    }
}
