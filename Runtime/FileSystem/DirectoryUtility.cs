using System.IO;
using System.Collections.Generic;

namespace SoulShard.FileSystem
{
    /// <summary>
    /// Contains multiple functions related to managing directories.
    /// </summary>
    public static class DirectoryUtility
    {
        #region Single Create And Delete
        /// <summary>
        /// creates a directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void Create(string path)
        {
            path = PathUtility.ParsePath(path);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// deletes a folder at a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void Delete(string path)
        {
            path = PathUtility.ParsePath(path);
            DeleteAllContents(path);
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete();
        }
        #endregion
        #region Delete All ___ In Dir
        /// <summary>
        /// deletes all contents (files and folders from a given directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void DeleteAllContents(string path)
        {
            path = PathUtility.ParsePath(path);
            DeleteAllFiles(path);
            DeleteAllFolders(path);
        }

        /// <summary>
        /// deletes all folders from a given directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void DeleteAllFolders(string path)
        {
            path = PathUtility.ParsePath(path);
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }

        /// <summary>
        /// deletes all files in a given directory
        /// </summary>
        /// <param name="path">the directory to perform this operation on</param>
        public static void DeleteAllFiles(string path)
        {
            path = PathUtility.ParsePath(path);
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
        }
        #endregion
        #region Get All ___ In Dir
        /// <summary>
        /// gets the paths of all directories within this directory
        /// </summary>
        /// <param name="path">the directory to search</param>
        /// <returns>the list of directories</returns>
        public static string[] GetAllDirectories(string path)
        {
            path = PathUtility.ParsePath(path);
            List<string> @return = new List<string>();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (DirectoryInfo dir in di.GetDirectories())
                @return.Add(dir.FullName);
            return @return.ToArray();
        }

        /// <summary>
        /// gets all file paths in a given directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns>an array of file paths</returns>
        public static string[] GetAllFilePaths(string path)
        {
            path = PathUtility.ParsePath(path);
            List<string> paths = new List<string>(0);
            if (!Directory.Exists(path))
                return null;
            foreach (var filePath in Directory.GetFiles(path))
                paths.Add(filePath);
            return paths.ToArray();
        }

        /// <summary>
        /// gets all file names in a given directory
        /// </summary>
        /// <param name="path">the directory to perform this operation with</param>
        /// <returns> an array of file names</returns>
        public static string[] GetAllFileNames(string path)
        {
            path = PathUtility.ParsePath(path);
            string[] paths = GetAllFilePaths(path);
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
        /// <param name="source">the directory to copy from</param>
        /// <param name="target">the directory to copy to</param>
        public static void Copy(string source, string target)
        {
            source = PathUtility.ParsePath(source);
            target = PathUtility.ParsePath(target);
            var diSource = new DirectoryInfo(source);
            var diTarget = new DirectoryInfo(target);
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
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        #endregion
        /// <summary>
        /// Whether a directory exists.
        /// </summary>
        /// <param name="directory">The directory to check.</param>
        /// <returns>Whether the directory exists.</returns>
        public static bool Exists(string directory)
        {
            directory = PathUtility.ParsePath(directory);
            return Directory.Exists(directory);
        }
    }
}
