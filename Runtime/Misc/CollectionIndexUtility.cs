using UnityEngine;
namespace SoulShard.Utils
{
    public struct CollectionIndexUtility
    {
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
    }
}