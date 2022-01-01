using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SoulShard.Utils
{
    public class AdvancedSceneManager : MonoBehaviour
    {
        /// <summary>
        /// the singleton reference for this class. you should have this class in a separate, always loaded scene.
        /// </summary>
        public static AdvancedSceneManager Instance { get; private set; }
        /// <summary>
        /// scenes loaded when this object is enabled
        /// </summary>
        [SerializeField] string[] _loadOnEnable;
        /// <summary>
        /// scenes that will always be loaded
        /// </summary>
        [SerializeField] string[] _loadAlways;
        void Awake()
        {
            Instance = this;
            SceneManager.sceneLoaded += OnLoad;
            SceneManager.sceneUnloaded += OnUnload;
            Load(_loadAlways);
            Load(_loadOnEnable);
        }
        #region Load
        /// <summary>
        /// loads a collection of scenes
        /// </summary>
        /// <param name="sceneNames"> the collection of scenes to load</param>
        public void Load(string[] sceneNames)
        {
            for (int i = 0; i < sceneNames.Length; i++)
                Load(sceneNames[i], LoadSceneMode.Additive);
        }
        /// <summary>
        /// loads a specific scene
        /// </summary>
        /// <param name="sceneName">the name of the scene</param>
        /// <param name="mode">the load scene mode</param>
        public void Load(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (!IsLoaded(sceneName))
                SceneManager.LoadScene(sceneName, mode);
        }
        #endregion
        #region Unload
        /// <summary>
        /// unloads a collection of scenes
        /// </summary>
        /// <param name="sceneNames">the collection of scenes to unload</param>
        public void Unload(string[] sceneNames)
        {
            for (int i = 0; i < sceneNames.Length; i++)
                Unload(sceneNames[i]);
        }
        /// <summary>
        /// unloads a specific scene
        /// </summary>
        /// <param name="sceneName">the name of the scene to unload</param>
        public void Unload(string sceneName)
        {
            if (!IsAlwaysLoadedScene(sceneName))
                if (IsLoaded(sceneName))
                    SceneManager.UnloadSceneAsync(sceneName);
        }
        /// <summary>
        /// unloads all scenes (barring all always active scenes)
        /// </summary>
        public void UnloadAll()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
                Unload(SceneManager.GetSceneAt(i).name);
        }
        #endregion
        #region On Scene State Change
        void OnLoad(Scene scene, LoadSceneMode mode)
        {
            CheckLoadWhileOtherIsActive();
            UpdateSceneChangeCallbacks(true);
        }
        void OnUnload(Scene scene)
        {
            CheckLoadWhileOtherIsActive();
            UpdateSceneChangeCallbacks(false);
        }
        #endregion
        #region Scene State Change Callback
        List<(string[], Action)> toExecuteOnSceneLoaded = new List<(string[], Action)>();
        List<(string[], Action)> toExecuteOnSceneUnloaded = new List<(string[], Action)>();
        /// <summary>
        /// add a callback for when a specific scene is done loading
        /// </summary>
        /// <param name="sceneName">the name of the scene to add a callback to</param>
        /// <param name="toPerform">the callback to perform when this event is triggered</param>
        public void AddSceneLoadedCallback(string sceneName, Action toPerform) =>
            toExecuteOnSceneLoaded.Add((new string[1] { sceneName }, toPerform));
        /// <summary>
        /// add a callback for when a specific scene is done unloading
        /// </summary>
        /// <param name="sceneName">the name of the scene to add a callback to</param>
        /// <param name="toPerform">the callback to perform when this event is triggered</param>
        public void AddSceneUnloadedCallback(string sceneName, Action toPerform) =>
            toExecuteOnSceneUnloaded.Add((new string[1] { sceneName }, toPerform));
        /// <summary>
        /// add a callback for if any one of the scenes is loaded
        /// </summary>
        /// <param name="sceneNames">the names of the scenes to add a callback to</param>
        /// <param name="toPerform">the action to perform when this event is triggered</param>
        public void AddSceneLoadedCallback(string[] sceneNames, Action toPerform) =>
            toExecuteOnSceneLoaded.Add((sceneNames, toPerform));
        /// <summary>
        /// add a callback for if any one of the scenes is unloaded
        /// </summary>
        /// <param name="sceneNames">the names of the scenes to add a callback to</param>
        /// <param name="toPerform">the action to perform when this event is triggered</param>
        public void AddSceneUnloadedCallback(string[] sceneNames, Action toPerform) =>
            toExecuteOnSceneUnloaded.Add((sceneNames, toPerform));
        void UpdateSceneChangeCallbacks(bool isloaded)
        {
            var toCheck = isloaded ? toExecuteOnSceneLoaded : toExecuteOnSceneUnloaded;
            for (int i = 0; i < toCheck.Count; i++)
            {
                if (IsLoaded(toCheck[i].Item1))
                {
                    toCheck[i].Item2?.Invoke();
                    toCheck.RemoveAt(i);
                }
            }
        }
        #endregion
        #region Is Loaded
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
        #region DependantScenes
        [SerializeField] SceneToScene[] _loadWhileOtherIsLoaded;
        /// <summary>
        /// get all dependant scenes for a specfic scene
        /// </summary>
        /// <param name="sceneName">the name of the scene to fetch the information from</param>
        /// <returns>the collection of dependant scenes</returns>
        public string[] GetDependantScenes(string sceneName)
        {
            List<string> @return = new List<string>();
            foreach (SceneToScene sts in _loadWhileOtherIsLoaded)
            {
                if (sts.OriginalScene == sceneName)
                    @return.Add(sts.DependantScene);
            }
            return @return.ToArray();
        }
        void CheckLoadWhileOtherIsActive()
        {
            foreach (SceneToScene sts in _loadWhileOtherIsLoaded)
            {
                bool originalLoaded = IsLoaded(sts.OriginalScene);
                if (originalLoaded)
                    Load(sts.DependantScene);
                else
                    Unload(sts.DependantScene);
            }
        }
        #endregion
        #region Other
        /// <summary>
        /// checks if a scene is a scene thats always loaded
        /// </summary>
        /// <param name="sceneName">the name of the scene to check</param>
        /// <returns>whether the scene is always loaded</returns>
        public bool IsAlwaysLoadedScene(string sceneName) => _loadAlways.Contains(sceneName);
        #endregion
        [Serializable]
        public struct SceneToScene
        {
            public string OriginalScene;
            public string DependantScene;
        }
    }
}
