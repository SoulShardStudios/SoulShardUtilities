using System;
namespace SoulShard.Utils
{
    /// <summary>
    /// A scene map that determines which scene is the original one, and which one is the dependant one. when the original scene is loaded, the dependent one will load as well. and the other way around.
    /// </summary>
    [Serializable]
    public struct SceneToScene
    {
        /// <summary>
        /// the original scene to track if its loaded
        /// </summary>
        public string originalScene;
        /// <summary>
        /// the dependant scene to load and unload when the original's state changes.
        /// </summary>
        public string dependantScene;
    }
}