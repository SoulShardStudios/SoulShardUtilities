using UnityEngine;
using System;
namespace SoulShard.Utils
{
    /// <summary>
    /// contains some basic functions to generate and manager collections
    /// </summary>
    public struct CollectionUtility
    {
        /// <summary>
        /// generates a new 2d array with a default value
        /// </summary>
        /// <typeparam name="T">the type of the collection</typeparam>
        /// <param name="xLength">the x length of the array</param>
        /// <param name="yLength">the y length of the array</param>
        /// <param name="defaultValue">the default value for the array</param>
        /// <returns>the new collection</returns>
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
        /// <summary>
        /// generates a new array with a default value
        /// </summary>
        /// <typeparam name="T">the type of the collection</typeparam>
        /// <param name="length">the length of the new collection</param>
        /// <param name="defaultValue">the default value of the collection</param>
        /// <returns>the new collection</returns>
        public static T[] GenerateNewArray<T>(int length, T defaultValue)
        {
            T[] @return = new T[length];
            for (int i = 0; i < length; i++)
                @return[i] = defaultValue;
            return @return;
        }
        /// <summary>
        /// gets a specific component from every gameobject in the list, and returns a list of the components.
        /// </summary>
        /// <typeparam name="T">the component to get from every object</typeparam>
        /// <param name="toGetComponentFrom">the array to get the components from</param>
        /// <returns>the collection of monobehaviors</returns>
        public static T[] GetComponentFromGameObjectList<T>(GameObject[] toGetComponentFrom) where T : MonoBehaviour
        {
            T[] @return = new T[toGetComponentFrom.Length];
            for (int i = 0; i < toGetComponentFrom.Length; i++)
                @return[i] = toGetComponentFrom[i].GetComponent<T>();
            return @return;
        }
        /// <summary>
        /// takes in a boolean list and checks if all the values are the same
        /// </summary>
        /// <typeparam name="T">the type of the collection</typeparam>
        /// <param name="list">the collection to compare</param>
        /// <returns>whether the list contains all of the same values</returns>
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
        /// <summary>
        /// generates a collection by inputting a collection's elements into a function and generating a new collection composed of the outputs.
        /// </summary>
        /// <typeparam name="_returnType">the return type of the function</typeparam>
        /// <typeparam name="_inputType">the input type of the function</typeparam>
        /// <param name="input">the input collection</param>
        /// <param name="func">the function to apply to all elements of the input collection</param>
        /// <returns>the newly generated collection</returns>
        public static _returnType[] GenerateCollectionFromFunction<_returnType, _inputType>(_inputType[] @input, Func<_inputType,_returnType> func)
        {
            _returnType[] @return = new _returnType[@input.Length];
            for (int i = 0; i < @input.Length; i++)
                @return[i] = func(@input[i]);
            return @return;
        }
        /// <summary>
        /// casts the input collection to the type of the output collection
        /// </summary>
        /// <typeparam name="_returnType">the type to cast the input collection to</typeparam>
        /// <typeparam name="_inputType">the input collection type to cast from</typeparam>
        /// <param name="input">the input collection to cast</param>
        /// <returns>the casted collection</returns>
        public static _returnType[] CastCollectionValues<_returnType, _inputType>(_inputType[] @input)
            where _inputType : IFormattable
        {
            _returnType[] @return = new _returnType[@input.Length];
            for (int i = 0; i < @input.Length; i++)
                TypeUtility.TryChangeTypeIFormattable(@input[i],out @return[i]);
            return @return;
        }
    }
}