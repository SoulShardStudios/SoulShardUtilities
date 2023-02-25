using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoulShard.Utils
{
    /// <summary>
    /// An advanced scene management utility. handles more advanced functionality at the price of an always loaded master scene, that you must put this singleton into.
    /// </summary>
    [DefaultExecutionOrder(-1000000)]
    public class AdvancedSceneManager : MonoBehaviour
    {
        /// <summary>
        /// the singleton reference for this class. you should have this class in a separate, always loaded scene.
        /// </summary>
        public static AdvancedSceneManager Instance { get; private set; }

        [SerializeField]
        AdvancedSceneManagerProps props;

        void Awake()
        {
            Instance = this;
            SceneManager.sceneLoaded += OnLoad;
            SceneManager.sceneUnloaded += OnUnload;
            Load(props.loadAlways);
            Load(props.loadOnEnable);
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
            if (!SceneUtility.IsLoaded(sceneName))
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
                if (SceneUtility.IsLoaded(sceneName))
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
                if (SceneUtility.IsLoaded(toCheck[i].Item1))
                {
                    toCheck[i].Item2?.Invoke();
                    toCheck.RemoveAt(i);
                }
            }
        }
        #endregion
        #region DependantScenes
        /// <summary>
        /// get all dependant scenes for a specfic scene
        /// </summary>
        /// <param name="sceneName">the name of the scene to fetch the information from</param>
        /// <returns>the collection of dependant scenes</returns>
        public string[] GetDependantScenes(string sceneName)
        {
            List<string> @return = new List<string>();
            foreach (SceneToScene sts in props.loadWhileOtherIsLoaded)
            {
                if (sts.originalScene == sceneName)
                    @return.Add(sts.dependantScene);
            }
            return @return.ToArray();
        }

        void CheckLoadWhileOtherIsActive()
        {
            foreach (SceneToScene sts in props.loadWhileOtherIsLoaded)
            {
                if (SceneUtility.IsLoaded(sts.dependantScene))
                    continue;
                if (SceneUtility.IsLoaded(sts.originalScene))
                    Load(sts.dependantScene);
                else
                    Unload(sts.dependantScene);
            }
        }
        #endregion
        #region Other
        /// <summary>
        /// checks if a scene is a scene thats always loaded
        /// </summary>
        /// <param name="sceneName">the name of the scene to check</param>
        /// <returns>whether the scene is always loaded</returns>
        public bool IsAlwaysLoadedScene(string sceneName) => props.loadAlways.Contains(sceneName);
        #endregion
    }
}
