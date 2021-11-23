using UnityEngine;
using System.IO;
using System.Collections.Generic;
namespace SoulShard.Utils
{
    /// <summary>
    /// stores various basic methods for managing a filesystem
    /// </summary>
    public static class FileSystemUtility
    {
        #region PathManagement
        /// <summary>
        /// turns stuff like <persistentdata>/directory/file.fil and turns it into the actual directory for the persistent data path plus whatever you added on. this is useful for serialising file paths
        /// </summary>
        /// <param name="path">the path to parse</param>
        /// <returns>the parsed path</returns>
        public static string ParsePath(string path)
        {
            if (path[0] != '<')
                return path;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != '>')
                    continue;
                return GetUnityPathFromString(path.Substring(1, i - 1)) + path.Substring(i + 1, path.Length - (i + 1));
            }
            return "";
        }
        /// <summary>
        /// the actual conversions from unity path name to unity path
        /// </summary>
        /// <param name="pathIndicator">the indicator for a unity path name</param>
        /// <returns>the unity path for the given indicator</returns>
        public static string GetUnityPathFromString(string pathIndicator)
        {
            return pathIndicator.ToLower() switch
            {
                "consolelog" => Application.consoleLogPath,
                "data" => Application.dataPath,
                "persistentdata" => Application.persistentDataPath,
                "streamingassets" => Application.streamingAssetsPath,
                "temporarycache" => Application.temporaryCachePath,
                _ => "",
            };
        }
        #endregion
        #region DirectoryManagement
        /// <summary>
        /// creates a directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void CreateDir(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
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
        /// deletes all contents (files and folders from a given directory
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void DeleteAllContentsInDirectory(string path)
        {
            DeleteAllFilesInDirectory(path);
            DeleteAllFoldersInDirectory(path);
        }
        /// <summary>
        /// deletes a folder at a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        public static void DeleteFolder(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete();
        }
        /// <summary>
        /// gets all file names in a given directory
        /// </summary>
        /// <param name="directory">the directory to perform this operation with</param>
        /// <returns> an array of file names</returns>
        public static string[] GetAllFileNamesInDirectory(string directory)
        {
            string[] paths = GetAllFilePathsInDirectory(directory);
            if (paths == null)
                return null;
            string[] names = new string[paths.Length];
            for (int i = 0; i < paths.Length; i++)
                names[i] = GetFileName(paths[i]);
            return names;
        }
        /// <summary>
        /// gets all file paths in a given directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>an array of file paths</returns>
        public static string[] GetAllFilePathsInDirectory(string directory)
        {
            List<string> paths = new List<string>(0);
            if (!Directory.Exists(directory))
                return null;
            foreach (var path in Directory.GetFiles(directory))
                paths.Add(path);
            return paths.ToArray();
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
        #region FileManagement
        /// <summary>
        /// makes a file at the given path with the given bytes
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <param name="bytes">the bytes to write to the file</param>
        public static void MakeFile(string path, byte[] bytes) => File.WriteAllBytes(path, bytes);
        /// <summary>
        /// deletes a file at a given path
        /// </summary>
        /// <param name="path">the path to perform this operation on</param>
        public static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            file.Delete();
        }
        /// <summary>
        /// gets the file name in a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the name of the file</returns>
        public static string GetFileName(string path) => Path.GetFileName(path);
        /// <summary>
        /// gets the file extension in a given path 
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the file extension</returns>
        public static string GetFileExt(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Extension;
        }
        /// <summary>
        /// checks if a file at the given path is readonly
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileIsReadonly(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.IsReadOnly;
        }
        #endregion
        #region AssetLoading
        /// <summary>
        /// loads a Texture2D from a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <param name="format">the format to set to the imported texture</param>
        /// <param name="mipChain">the mipchain value to set to the imported texture</param>
        /// <returns>the imported Texture2D</returns>
        public static Texture2D LoadTexture2D(string path, TextureFormat format, bool mipChain) => LoadTexture2DRawPath(ParsePath(path), format, mipChain);
        /// <summary>
        /// loads a Texture2D from a given raw path with no path preprocessing
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <param name="format">the format to set to the imported texture</param>
        /// <param name="mipChain">the mipchain value to set to the imported texture</param>
        /// <returns>the imported Texture2D</returns>
        public static Texture2D LoadTexture2DRawPath(string path, TextureFormat format, bool mipChain)
        {
            if (!File.Exists(path))
                return null;
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            TextureUtility.ConvertTexture2DFormat(ref tex, format, mipChain);
            return tex;
        }
        /// <summary>
        /// loads the text from a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the contents of the file in text form</returns>
        public static string LoadText(string path) => LoadTextRawPath(ParsePath(path));
        /// <summary>
        /// loads the text from a given path with no path preprocessing
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the contents of the file in text form</returns>
        public static string LoadTextRawPath(string path)
        {
            if (!File.Exists(path))
                return null;
            return File.ReadAllText(path);
        }
        #endregion
    }
}