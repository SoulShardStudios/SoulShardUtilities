namespace SoulShard.Utils
{
    public static class JobHelper
    {
        public static int GetBatchAmount(int size, int partitions, int manualPartition)
        {
            int batchCount = manualPartition > 0 ? manualPartition : size / partitions;
            if (batchCount == 0)
                batchCount = 1;
            return batchCount;
        }
    }
}