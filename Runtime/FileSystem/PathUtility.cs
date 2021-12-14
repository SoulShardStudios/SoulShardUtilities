using UnityEngine;
using SoulShard.Utils;
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
        /// <param name="delim">the delimiter</param>
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
        /// <summary>
        /// gets an element of a path. EX: C:/builds/test0, this function allows you to isolate whats at the end of the path, the middle, e.t.c.
        /// </summary>
        /// <param name="path">the path to analyze</param>
        /// <param name="index">the index in the path</param>
        /// <param name="dir">the direction to search from. false is search from end to start, and true does the opposite</param>
        /// <param name="delim">the delimiter of the path</param>
        /// <returns>the element in that position of the path</returns>
        public static string GetPartOfPath(string path, int index, bool dir, char delim)
        {
            string @return = "";
            int numberOfElements = StringUtility.GetNumberOfCharsInStr(path, delim);
            if (numberOfElements < index)
                return null;
            int currentIndex = 0;
            for (int i = dir ? 0 : path.Length - 1; dir ? i < path.Length : i > -1; i += dir ? 1 : -1)
            {
                if (path[i] == delim)
                {
                    currentIndex++;
                    continue;
                }
                if (currentIndex == index)
                {
                    @return += path[i];
                }
            }
            if (!dir)
                StringUtility.ReverseString(ref @return);
            return @return;
        }
    }
}