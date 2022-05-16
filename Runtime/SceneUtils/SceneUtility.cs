using UnityEngine.SceneManagement;

namespace SoulShard.Utils
{
    /// <summary>
    /// contains some better implementations of specific unity scene management features.
    /// </summary>
    public static class SceneUtility
    {
        #region IsLoaded
        /// <summary>
        /// checks if the specified scene is loaded
        /// </summary>
        /// <param name="sceneName">the name of the scene to check</param>
        /// <returns>whether the scene is loaded</returns>
        public static bool IsLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// checks if the collection of scenes is loaded
        /// </summary>
        /// <param name="sceneNames">the names of the scenes to check</param>
        /// <returns>whether the collection of scenes is all loaded</returns>
        public static bool IsLoaded(string[] sceneNames)
        {
            bool isLoaded = true;
            foreach (string sceneName in sceneNames)
                if (!IsLoaded(sceneName))
                    isLoaded = false;
            return isLoaded;
        }
        #endregion
    }
}
