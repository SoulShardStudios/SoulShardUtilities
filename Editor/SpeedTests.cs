using UnityEngine;
using System;
namespace SoulShard.Utils
{
    public static class SpeedTests
    {
        public static (float, float) SpeedTest(Action a1, Action a2)
        {
            float _start = Time.realtimeSinceStartup;
            a1?.Invoke();
            float _switch = Time.realtimeSinceStartup;
            a2?.Invoke();
            float _end = Time.realtimeSinceStartup;
            return ((_switch - _start) * 1000f, (_end - _switch) * 1000f);
        }
        public static void SpeedTestDebug(Action a1, Action a2)
        {
            (float, float) speedTestResults = SpeedTest(a1, a2);
            Debug.Log(("In Miliseconds", "A", speedTestResults.Item1, "B", speedTestResults.Item2));
        }
    }
}