using UnityEngine;
namespace SoulShard.Utils
{
    public static class DirectoryHelp
    {
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
        static string GetUnityPathFromString(string pathIndicator)
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
    }
}