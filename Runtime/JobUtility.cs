using Unity.Collections;
namespace SoulShard.Utils
{
    /// <summary>
    /// stores some basic functions to aid in job execution and scheduling
    /// </summary>
    public struct JobUtility
    {
        /// <summary>
        /// gets the batches for an iparralelfor job. 
        /// </summary>
        /// <param name="size">the size of the collection for the iparralelfor</param>
        /// <param name="partitions">the number of default partitions</param>
        /// <param name="manualPartition">is there a manual partitioning size? if so use that instead</param>
        /// <returns></returns>
        public static int GetBatchAmount(int size, int partitions, int manualPartition = -1)
        {
            int batchCount = manualPartition > 0 ? manualPartition : size / partitions;
            if (batchCount == 0)
                batchCount = 1;
            return batchCount;
        }
        /// <summary>
        /// a quicker way to add an array to a native list, as currently you cannot just add a raw array.
        /// </summary>
        /// <typeparam name="T">the type of the list</typeparam>
        /// <param name="nativeList">the native list to add the collection to</param>
        /// <param name="arr">the array that will be added to the nativelist</param>
        public static void AddToNativeList<T>(NativeList<T> nativeList, T[] arr)
            where T: unmanaged
        {
            NativeArray<T> n_arr = new NativeArray<T>(arr, Allocator.Temp);
            nativeList.AddRange(n_arr);
            n_arr.Dispose();
        }

    }
}