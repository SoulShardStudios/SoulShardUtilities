using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoulShard.Utils
{
    /// <summary>
    /// An advanced scene management utility.
    /// handles more advanced functionality at the price of an always loaded master scene,
    /// that you must put this singleton into.
    /// </summary>
    [DefaultExecutionOrder(-1000000)]
    public class AdvancedSceneManager : MonoBehaviour
    {
        [SerializeField]
        AdvancedSceneManagerProps props;

        static AdvancedSceneManager _instance;

        string _activeScene;

        protected virtual void Awake()
        {
            _instance = this;
            _activeScene = props.startingActiveScene;
            Load(
                _activeScene,
                () =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(_activeScene));
                }
            );
            Load(props.loadAlways);
            Load(props.loadOnEnable);
        }

        #region Load Unload

        /// <summary>
        /// loads a collection of scenes
        /// </summary>
        /// <param name="sceneNames"> the collection of scenes to load</param>
        public static void Load(string[] sceneNames, Action onComplete = null)
        {
            if (onComplete != null)
                _instance._sceneLoadCallbacks.Add((sceneNames, onComplete));
            for (int i = 0; i < sceneNames.Length; i++)
                Load(sceneNames[i]);
        }

        /// <summary>
        /// loads a specific scene
        /// </summary>
        /// <param name="sceneName">the name of the scene</param>
        /// <param name="mode">the load scene mode</param>
        public static void Load(
            string sceneName,
            Action onComplete = null,
            LoadSceneMode mode = LoadSceneMode.Additive
        )
        {
            if (onComplete != null)
                _instance._sceneLoadCallbacks.Add((new string[1] { sceneName }, onComplete));
            if (!IsInstantiated(sceneName))
                SceneManager.LoadSceneAsync(sceneName, mode);
        }

        /// <summary>
        /// Unloads the previous active scene and loads the next active scene
        /// </summary>
        /// <param name="newActiveScene">The new active scene to transition to</param>
        /// <param name="onComplete">Callback for when the loading is complete</param>
        public static void ActiveSceneTransition(string newActiveScene, Action onComplete = null)
        {
            Unload(
                _instance._activeScene,
                () =>
                {
                    Load(
                        newActiveScene,
                        () =>
                        {
                            SceneManager.SetActiveScene(
                                SceneManager.GetSceneByName(newActiveScene)
                            );
                            onComplete?.Invoke();
                            _instance._activeScene = newActiveScene;
                        }
                    );
                }
            );
        }

        /// <summary>
        /// unloads a collection of scenes
        /// </summary>
        /// <param name="sceneNames">the collection of scenes to unload</param>
        public static void Unload(string[] sceneNames, Action onComplete = null)
        {
            if (onComplete != null)
                _instance._sceneUnloadCallbacks.Add((sceneNames, onComplete));
            for (int i = 0; i < sceneNames.Length; i++)
                Unload(sceneNames[i]);
        }

        /// <summary>
        /// unloads a specific scene
        /// </summary>
        /// <param name="sceneName">the name of the scene to unload</param>
        public static void Unload(string sceneName, Action onComplete = null)
        {
            if (onComplete != null)
                _instance._sceneUnloadCallbacks.Add((new string[1] { sceneName }, onComplete));
            if (!IsAlwaysLoadedScene(sceneName))
                if (IsLoaded(sceneName))
                    SceneManager.UnloadSceneAsync(sceneName);
        }

        /// <summary>
        /// unloads all scenes (barring all always active scenes)
        /// </summary>
        public static void UnloadAll(Action onComplete = null)
        {
            var allLoadedScenes = GetAllLoadedSceneNames();
            if (onComplete != null)
                _instance._sceneUnloadCallbacks.Add((allLoadedScenes, onComplete));
            foreach (var name in allLoadedScenes)
                Unload(name);
        }
        #endregion

        #region Callbacks

        List<(string[], Action)> _sceneLoadCallbacks = new List<(string[], Action)>();
        List<(string[], Action)> _sceneUnloadCallbacks = new List<(string[], Action)>();

        protected virtual void Update()
        {
            for (int i = _instance._sceneLoadCallbacks.Count - 1; i > -1; i--)
            {
                if (IsLoaded(_instance._sceneLoadCallbacks[i].Item1))
                {
                    _instance._sceneLoadCallbacks[i].Item2?.Invoke();
                    _instance._sceneLoadCallbacks.RemoveAt(i);
                }
            }
            for (int i = _instance._sceneUnloadCallbacks.Count - 1; i > -1; i--)
            {
                if (!IsLoaded(_instance._sceneUnloadCallbacks[i].Item1))
                {
                    _instance._sceneUnloadCallbacks[i].Item2?.Invoke();
                    _instance._sceneUnloadCallbacks.RemoveAt(i);
                }
            }
        }
        #endregion

        #region Loaded

        /// <summary>
        /// checks if a scene is a scene thats always loaded
        /// </summary>
        /// <param name="sceneName">the name of the scene to check</param>
        /// <returns>whether the scene is always loaded</returns>
        public static bool IsAlwaysLoadedScene(string sceneName) =>
            _instance.props.loadAlways.Contains(sceneName);

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
                if (scene.name == sceneName && scene.isLoaded)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether the specified scene has been instantiated into the world
        /// </summary>
        /// <param name="sceneName">The name of the scene to check for</param>
        /// <returns>Whether the scene has been instantiated</returns>
        public static bool IsInstantiated(string sceneName)
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

        public static string[] GetAllLoadedSceneNames()
        {
            string[] sceneNames = new string[SceneManager.sceneCount];
            for (int i = 0; i < SceneManager.sceneCount; i++)
                sceneNames[i] = SceneManager.GetSceneAt(i).name;
            return sceneNames;
        }
        #endregion
    }
}
