using UnityEngine;

namespace SoulShard.FileSystem
{
    /// <summary>
    /// utility for managing file explorer prompts
    /// </summary>
    public static class ExplorerUtility
    {
        /// <summary>
        /// this opens file explorer at the specified path
        /// </summary>
        /// <param name="path">the path to open file explorer at</param>
        public static void OpenFileExplorerAtPath(string path)
        {
            path = PathUtility.ParsePath(path);
            path = path.Replace("/", @"\");
            System.Diagnostics.Process.Start(path);
        }
    }
}