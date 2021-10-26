using UnityEngine;
using System;
namespace SoulShard.Utils
{
    public static class SpeedTests
    {
        public const string Units = "In Miliseconds";
        public static float GetDiffInMS(float a, float b) => Mathf.Abs(a - b) * 1000f;
        public static float SpeedTest(Action a)
        {
            float _start = Time.realtimeSinceStartup;
            a?.Invoke();
            float _end = Time.realtimeSinceStartup;
            return (_end- _start) * 1000f;
        }
        public static void SpeedTestDebug(Action a, string taskName)
        {
            float speedTestResults = SpeedTest(a);
            Debug.Log((taskName, Units, speedTestResults));
        }

        public static float[] SpeedTestDebug(Action[] actions)
        {
            float[] diffs = new float[actions.Length];
            for(int i = 0; i < actions.Length; i++)
            {
                float start = Time.realtimeSinceStartup;
                actions[i]?.Invoke();
                diffs[i] = GetDiffInMS(Time.realtimeSinceStartup, start);
            }
            return diffs;
        }
        public static void SpeedTestDebug(Action[] actions, string[] taskNames = null, string? testName = null)
        {
            float[] speedTestResults = SpeedTestDebug(actions);
            Debug.Log(testName != null ? testName : "Test");
            for(int i = 0; i < speedTestResults.Length; i++)
                Debug.Log((taskNames?[i] ?? i.ToString(), Units, speedTestResults[i]));
        }


    }
}