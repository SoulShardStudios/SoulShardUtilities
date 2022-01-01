using System;
namespace SoulShard.Utils
{
    /// <summary>
    /// contains various helper functions for modifying strings
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        /// reverses a string. hahaha becomes ahahah
        /// </summary>
        /// <param name="str">the string to reverse</param>
        /// <returns>the reversed string</returns>
        public static string ReverseString(ref string str) => str = ReverseString(str);
        /// <summary>
        /// reverses a string. hahaha becomes ahahah
        /// </summary>
        /// <param name="str">the string to reverse</param>
        /// <returns>the reversed string</returns>
        public static string ReverseString(string str)
        {
            char[] chararr = str.ToCharArray();
            Array.Reverse(chararr);
            return new string(chararr);
        }
        /// <summary>
        /// gets the number of characters in a string
        /// </summary>
        /// <param name="str">the string to analyze</param>
        /// <param name="charToCount">the character to count in the string</param>
        /// <returns>the number of char occurences</returns>
        public static int GetNumberOfCharsInStr(string str, char charToCount)
        {
            int @return = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i] == charToCount)
                    @return++;
            return @return;
        }
        /// <summary>
        /// removes all content in the string past the size value.
        /// </summary>
        /// <param name="str">the string to clamp</param>
        /// <param name="size">the size of the returned string</param>
        /// <returns>the clamped string</returns>
        public static string ClampStringSize(string str, int size)
        {
            size = str.Length >= size ? size : str.Length;
            return str.Substring(0, size);
        }
    }
}
