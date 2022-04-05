using System.IO;
using System.Collections.Generic;
namespace SoulShard.FileSystem
{
    /// <summary>
    /// contains multiple functions related to managing directories
    /// </summary>
    public static class DirectoryUtility
    {
        #region Single Create And Delete
        /// <summary>
        /// creates a directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void CreateDir(string path)
        {
            path = PathUtility.ParsePath(path);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        /// <summary>
        /// deletes a folder at a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void DeleteDir(string path)
        {
            path = PathUtility.ParsePath(path);
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete();
        }
        #endregion
        #region Delete All ___ In Dir
        /// <summary>
        /// deletes all contents (files and folders from a given directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void DeleteAllContentsInDirectory(string path)
        {
            DeleteAllFilesInDirectory(path);
            DeleteAllFoldersInDirectory(path);
        }
        /// <summary>
        /// deletes all folders from a given directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void DeleteAllFoldersInDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }
        /// <summary>
        /// deletes all files in a given directory
        /// </summary>
        /// <param name="directory">the directory to perform this operation on</param>
        public static void DeleteAllFilesInDirectory(string directory)
        {
            DirectoryInfo di = new DirectoryInfo(directory);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
        }
        #endregion
        #region Get All ___ In Dir
        /// <summary>
        /// gets the paths of all directories within this directory
        /// </summary>
        /// <param name="directory">the directory to search</param>
        /// <returns>the list of directories</returns>
        public static string[] GetAllDirectories(string directory)
        {
            directory = PathUtility.ParsePath(directory);
            List<string> @return = new List<string>();
            DirectoryInfo di = new DirectoryInfo(directory);
            foreach (DirectoryInfo dir in di.GetDirectories())
                @return.Add(dir.FullName);
            return @return.ToArray();
        }
        /// <summary>
        /// gets all file paths in a given directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>an array of file paths</returns>
        public static string[] GetAllFilePathsInDirectory(string directory)
        {
            directory = PathUtility.ParsePath(directory);
            List<string> paths = new List<string>(0);
            if (!Directory.Exists(directory))
                return null;
            foreach (var path in Directory.GetFiles(directory))
                paths.Add(path);
            return paths.ToArray();
        }
        /// <summary>
        /// gets all file names in a given directory
        /// </summary>
        /// <param name="directory">the directory to perform this operation with</param>
        /// <returns> an array of file names</returns>
        public static string[] GetAllFileNamesInDirectory(string directory)
        {
            directory = PathUtility.ParsePath(directory);
            string[] paths = GetAllFilePathsInDirectory(directory);
            if (paths == null)
                return null;
            string[] names = new string[paths.Length];
            for (int i = 0; i < paths.Length; i++)
                names[i] = FileUtility.GetName(paths[i]);
            return names;
        }
        #endregion
        #region Copy
        // thanks to https://code.4noobz.net/c-copy-a-folder-its-content-and-the-subfolders/
        /// <summary>
        /// copies all contents of the source directory to the target directory
        /// </summary>
        /// <param name="sourceDirectory">the directory to copy from</param>
        /// <param name="targetDirectory">the directory to copy to</param>
        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            sourceDirectory = PathUtility.ParsePath(sourceDirectory);
            targetDirectory = PathUtility.ParsePath(targetDirectory);
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);
            CopyAll(diSource, diTarget);
        }
        /// <summary>
        /// copies all contents of the source directory to the target directory
        /// </summary>
        /// <param name="source">the directory to copy from</param>
        /// <param name="target">the directory to copy to</param>
        static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.GetFiles())
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        #endregion
    }
}
