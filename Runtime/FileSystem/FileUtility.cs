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
        #endregion
        #region GetFileAttributes
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
    }
}
