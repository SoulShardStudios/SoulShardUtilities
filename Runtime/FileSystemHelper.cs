using UnityEngine;
using System.IO;
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
            switch (pathIndicator.ToLower())
            {
                case "consolelog":
                    return Application.consoleLogPath;
                case "data":
                    return Application.dataPath;
                case "persistentdata":
                    return Application.persistentDataPath;
                case "streamingassets":
                    return Application.streamingAssetsPath;
                case "temporarycache":
                    return Application.temporaryCachePath;
                default:
                    return "";
            }
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
        #endregion
        #region FileManagement
        public static void DeleteAllFilesInDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
        }
        public static void MakeFile(string path, byte[] bytes) => File.WriteAllBytes(path, bytes);
        public static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            file.Delete();
        }
        #endregion
        #region AssetLoading
        // Original Source: https://gist.github.com/openroomxyz
        public static Texture2D LoadTexture2D(string filePath) => LoadTexture2DRawPath(ParsePath(filePath));
        public static Texture2D LoadTexture2DRawPath(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
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