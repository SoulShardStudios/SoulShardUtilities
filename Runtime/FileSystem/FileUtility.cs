using System.IO;
namespace SoulShard.FileSystem
{
    /// <summary>
    /// contains multiple functions related to managing files
    /// </summary>
    public class FileUtility
    {
        #region Single Make and Delete
        /// <summary>
        /// Makes a file at the given path with the given bytes.
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <param name="bytes">the bytes to write to the file</param>
        public static void Make(string path, byte[] bytes) => File.WriteAllBytes(PathUtility.ParsePath(path), bytes);
        /// <summary>
        /// Makes a file with the given text.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="text">The text of the file.</param>
        public static void Make(string path, string text) => File.WriteAllText(PathUtility.ParsePath(path), text);
        /// <summary>
        /// deletes a file at a given path
        /// </summary>
        /// <param name="path">the path to perform this operation on</param>
        public static void Delete(string path)
        {
            path = PathUtility.ParsePath(path);
            FileInfo file = new FileInfo(path);
            file.Delete();
        }
        #endregion
        #region GetFileAttributes
        /// <summary>
        /// gets the file name in a given path
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the name of the file</returns>
        public static string GetName(string path) => Path.GetFileName(path);
        /// <summary>
        /// gets the file extension in a given path 
        /// </summary>
        /// <param name="path">the path to perform this operation with</param>
        /// <returns>the file extension</returns>
        public static string GetExt(string path)
        {
            path = PathUtility.ParsePath(path);
            FileInfo fi = new FileInfo(path);
            return fi.Extension;
        }
        /// <summary>
        /// Checks if a file at the given path is readonly
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>Whether the file is readonly.</returns>
        public static bool IsReadonly(string path)
        {
            path = PathUtility.ParsePath(path);
            FileInfo fi = new FileInfo(path);
            return fi.IsReadOnly;
        }
        /// <summary>
        /// Checks if the file at the given path exists.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>Whether the file exists.</returns>
        public static bool Exists(string path)
        {
            path = PathUtility.ParsePath(path);
            FileInfo fi = new FileInfo(path);
            return fi.Exists;
        }
        #endregion
    }
}
