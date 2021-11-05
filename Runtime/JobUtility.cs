namespace SoulShard.Utils
{
    public struct JobUtility
    {
        public static int GetBatchAmount(int size, int partitions, int manualPartition = -1)
        {
            int batchCount = manualPartition > 0 ? manualPartition : size / partitions;
            if (batchCount == 0)
                batchCount = 1;
            return batchCount;
        }
    }
}