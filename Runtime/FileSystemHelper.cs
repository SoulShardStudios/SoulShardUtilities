using UnityEngine;
using System.IO;
using System.Collections.Generic;
namespace SoulShard.Utils
{
    public static class FileSystemHelper
    {
        #region PathManagement
        // turns stuff like <persistentdata>/directory/file.fil and turns it into the actual directory for the persistent data path plus whatever you added on
        // this is useful for serializing these paths in the inspector by creating a shorthand for these variables.
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
        // the actual conversions from unity path name to unity path
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
        public static void CreateDir(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public static void DeleteAllFoldersInDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }
        public static void DeleteAllContentsInDirectory(string path)
        {
            DeleteAllFilesInDirectory(path);
            DeleteAllFoldersInDirectory(path);
        }
        public static void DeleteFolder(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete();
        }
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
        public static string[] GetAllFilePathsInDirectory(string directory)
        {
            List<string> paths = new List<string>(0);
            if (!Directory.Exists(directory))
                return null;
            foreach (var path in Directory.GetFiles(directory))
                paths.Add(path);
            return paths.ToArray();
        }
        public static void DeleteAllFilesInDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
        }
        #endregion
        #region FileManagement
        public static void MakeFile(string path, byte[] bytes) => File.WriteAllBytes(path, bytes);
        public static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            file.Delete();
        }
        public static string GetFileName(string path) => Path.GetFileName(path);
        public static string GetFileExt(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Extension;
        }
        public static bool FileIsReadonly(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.IsReadOnly;
        }
        #endregion
        #region AssetLoading
        // Original Source: https://gist.github.com/openroomxyz
        public static Texture2D LoadTexture2D(string filePath, TextureFormat format, bool mipChain) => LoadTexture2DRawPath(ParsePath(filePath), format, mipChain);
        public static Texture2D LoadTexture2DRawPath(string filePath, TextureFormat format, bool mipChain)
        {
            if (!File.Exists(filePath))
                return null;
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            TextureHelp.ConvertTexture2DFormat(ref tex, format, mipChain);
            return tex;
        }
        public static string LoadText(string filePath) => LoadTextRawPath(ParsePath(filePath));
        public static string LoadTextRawPath(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            return File.ReadAllText(filePath);
        }
        #endregion
    }
}