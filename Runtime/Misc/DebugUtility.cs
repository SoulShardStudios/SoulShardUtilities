using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SoulShard.Utils
{
    public static class DebugUtility
    {
        public const string Units = "In Miliseconds";
        /// <summary>
        /// gets the difference between two points in time in Milliseconds
        /// </summary>
        /// <param name="a">ending time</param>
        /// <param name="b">starting time</param>
        /// <returns>the difference in milliseconds</returns>
        public static float GetDiffInMS(float a, float b) => Mathf.Abs(a - b) * 1000f;
        public static float SpeedTest(Action a)
        {
            float _start = Time.realtimeSinceStartup;
            a?.Invoke();
            float _end = Time.realtimeSinceStartup;
            return GetDiffInMS(_start, _end);
        }
        /// <summary>
        /// debugs the completion time and operation name for the given task
        /// </summary>
        /// <param name="a">the task to test for</param>
        /// <param name="taskName">the task name</param>
        public static void SpeedTestDebug(Action a, string taskName)
        {
            float speedTestResults = SpeedTest(a);
            Debug.Log((taskName, Units, speedTestResults));
        }

        /// <summary>
        /// gets the completion times for a list of actions
        /// </summary>
        /// <param name="actions">the list of actions to test for</param>
        /// <returns>the list of completion times</returns>
        public static float[] SpeedTest(Action[] actions)
        {
            float[] diffs = new float[actions.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                float start = Time.realtimeSinceStartup;
                actions[i]?.Invoke();
                diffs[i] = GetDiffInMS(Time.realtimeSinceStartup, start);
            }
            return diffs;
        }
        /// <summary>
        /// debugs the completion times for a list of actions
        /// </summary>
        /// <param name="actions">the list tasks to test for</param>
        /// <param name="taskNames">the names of the tasks</param>
        /// <param name="testName">the name of the entire test</param>
        public static void SpeedTestDebug(Action[] actions, string[] taskNames = null, string? testName = null)
        {
            float[] speedTestResults = SpeedTest(actions);
            Debug.Log(testName != null ? testName : "Test");
            for (int i = 0; i < speedTestResults.Length; i++)
                Debug.Log((taskNames?[i] ?? i.ToString(), Units, speedTestResults[i]));
        }
        /// <summary>
        /// Debugs the contents of a collection
        /// </summary>
        /// <typeparam name="T">the collection type</typeparam>
        /// <param name="collection">the collection to debug</param>
        public static void LogCollection<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                Debug.Log(null);
                return;
            }
            string ToDebug = "";
            T[] arr = collection.ToArray();
            for (int i = 0; i < collection.Count(); i++)
            {
                string comma = (i + 1 != collection.Count() ? ", " : "");
                ToDebug += arr[i]?.ToString() + comma;
            }
            Debug.Log(ToDebug);
        }
        /// <summary>
        /// Debugs the contents of a collection
        /// </summary>
        /// <typeparam name="T">the collection type</typeparam>
        /// <param name="collection">the collection to debug</param>
        public static void LogCollection<T>(IEnumerable<T> collection, Func<T,string> stringConversion)
        {
            if (collection == null)
            {
                Debug.Log(null);
                return;
            }
            string ToDebug = "";
            T[] arr = collection.ToArray();
            for (int i = 0; i < collection.Count(); i++)
            {
                string comma = (i + 1 != collection.Count() ? ", " : "");
                ToDebug += stringConversion(arr[i]) + comma;
            }
            Debug.Log(ToDebug);
        }
    }
}