using UnityEngine;
namespace SoulShard.FileSystem
{
    /// <summary>
    /// contains functions for managing individual paths
    /// </summary>
    public static class PathUtility
    {
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
        /// <summary>
        /// removes the file from a path. EX: C:/dev/test.txt becomes C:/dev
        /// </summary>
        /// <param name="path">the path to modify</param>
        /// <param name="delim">the character separating directories EX: C:/dev/text.txt '/' is the delimiter</param>
        /// <returns>the modified path</returns>
        public static string RemoveFileFromPath(string path, char delim)
        {
            for (int i = path.Length - 1; i > -1; i--)
            {
                if (path[i] == delim)
                    return path.Substring(0, i);
            }
            return path;
        }
    }
}