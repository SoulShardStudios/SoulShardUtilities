using UnityEngine;
namespace SoulShard.Utils
{
    public static class CollectionUtility
    {
        // returns a new 2d array, initialized with a default value.
        public static T[,] GenerateNew2dArray<T>(int xLength, int yLength, T defaultValue) where T : new()
        {
            T[,] @return = new T[xLength, yLength];
            if (defaultValue == null)
                return null;
            for (int i = 0; i < xLength; i++)
            {
                for (int e = 0; e < yLength; e++)
                    @return[i, e] = defaultValue;
            }
            return @return;
        }

        // generates a new 2d array.
        public static T[,] GenerateNew2dArray<T>(int xLength, int yLength) => new T[xLength, yLength];
        // generates a new array with a default value
        public static T[] GenerateNewArray<T>(int length, T defaultValue)
        {
            T[] @return = new T[length];
            for (int i = 0; i < length; i++)
                @return[i] = defaultValue;
            return @return;
        }
        // gets a specific component from every gameobject in the list, and returns a list of the components.
        public static T[] GetComponentFromGameObjectList<T>(GameObject[] toGetComponentFrom) where T : MonoBehaviour
        {
            T[] @return = new T[toGetComponentFrom.Length];
            for (int i = 0; i < toGetComponentFrom.Length; i++)
                @return[i] = toGetComponentFrom[i].GetComponent<T>();
            return @return;
        }
        // takes in a boolean list and checks if all the values are the same
        public static bool? ListIsRepeatedValues<T>(T[] list) where T: System.IEquatable<T>
        {
            if (list.Length == 0)
                return null;
            T start = list[0];
            foreach (T t in list)
                if (!t.Equals(start))
                    return false;
            return true;
        }
    }
}