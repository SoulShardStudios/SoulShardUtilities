using System;
using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// contains some basic functions to generate and manager collections
    /// </summary>
    public struct CollectionUtility
    {
        #region Other
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
        /// <param name="compareTo">the value to compare the collection to</param>
        /// <returns>whether the list contains all of the same values</returns>
        public static bool? CollectionIsEqualToValue<T>(T[] list, T compareTo) where T : IEquatable<T>
        {
            if (list.Length == 0)
                return null;
            foreach (T t in list)
                if (!t.Equals(compareTo))
                    return false;
            return true;
        }
        #endregion
        #region Collection Generation
        /// <summary>
        /// generates a collection by inputting a collection's elements into a function and generating a new collection composed of the outputs.
        /// </summary>
        /// <typeparam name="_returnType">the return type of the function</typeparam>
        /// <typeparam name="_inputType">the input type of the function</typeparam>
        /// <param name="input">the input collection</param>
        /// <param name="func">the function to apply to all elements of the input collection</param>
        /// <returns>the newly generated collection</returns>
        public static _returnType[] GenerateCollectionFromFunction<_returnType, _inputType>(_inputType[] @input, Func<_inputType, _returnType> func)
        {
            _returnType[] @return = new _returnType[@input.Length];
            for (int i = 0; i < @input.Length; i++)
                @return[i] = func(@input[i]);
            return @return;
        }
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
        #endregion
        #region Index Management Methods
        /// <summary>
        /// checks whether an index exists within a collection
        /// </summary>
        /// <param name="index">the index within the collection</param>
        /// <param name="collectionSize">the size of the collection</param>
        /// <returns></returns>
        public static bool IndexExists(int index, int collectionSize) =>
            index < collectionSize && index >= 0;

        #region 2D flattened array helpers, ydominant
        /// <summary>
        /// gets the index within a flattened 2d array from a 2d position in that array
        /// </summary>
        /// <param name="pos"> the 2d position within the array</param>
        /// <param name="collectionSize"> the size of the collection along the virtual x axis</param>
        /// <returns>the index within the collection</returns>
        public static int GetIndex(Vector2Int pos, int collectionSize) =>
            pos.x + pos.y * collectionSize;
        /// <summary>
        /// returns the 2d position from an index within the flattened 2d collection
        /// </summary>
        /// <param name="index">the index within the collection</param>
        /// <param name="collectionSize">the size of the collection along the virtual x axis</param>
        /// <returns>the 2d position this index represents</returns>
        public static Vector2Int GetPosition(int index, int collectionSize) =>
            new Vector2Int(index % collectionSize, (int)Mathf.Floor(index / collectionSize));
        /// <summary>
        /// checks if a position exists within the 2d collection
        /// </summary>
        /// <param name="position">the position within the collection</param>
        /// <param name="size">the size of the collection</param>
        /// <returns></returns>
        public static bool PositionExists(Vector2Int position, Vector2Int size) =>
            position.x < size.x && position.y < size.y;
        #endregion
        #endregion
    }
}