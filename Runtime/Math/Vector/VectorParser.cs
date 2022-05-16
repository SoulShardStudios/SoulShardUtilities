using UnityEngine;

namespace SoulShard.Math
{
    /// <summary>
    /// parses all unity vector types from strings with the format (axis1, axis2, e.t.c.)
    /// </summary>
    public struct VectorParser
    {
        // parses a string to find a vector var EX: "THIS IS A TEST STRING (0,100)" where (0,100) is the vector. it removes everything but 0,100
        // and returns that for further processing and possible conversion
        static string GetVectorInnerString(string str)
        {
            int[] sliceIndicies = new int[2];
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                    sliceIndicies[0] = i;
                if (str[i] == ')')
                    sliceIndicies[1] = i;
            }
            return str.Substring(sliceIndicies[0] + 1, sliceIndicies[1] - sliceIndicies[0] - 1);
        }

        #region Parse Functions
        /// <summary>
        /// Parses a string so that it may be converted to the desired vector.
        /// format: (x,y,e.t.c.)
        /// </summary>
        /// <param name="str"> the input string to parse into a vector </param>
        /// <returns>The parsed vector</returns>
        public static Vector2Int ParseVector2IntFromString(string str)
        {
            string[] temp = GetVectorInnerString(str).Split(',');
            return new Vector2Int(int.Parse(temp[0]), int.Parse(temp[1]));
        }

        /// <summary>
        /// Parses a string so that it may be converted to the desired vector.
        /// format: (x,y,e.t.c.)
        /// </summary>
        /// <param name="str"> the input string to parse into a vector </param>
        /// <returns>The parsed vector</returns>
        public static Vector2 ParseVector2FromString(string str)
        {
            string[] temp = GetVectorInnerString(str).Split(',');
            return new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
        }

        /// <summary>
        /// Parses a string so that it may be converted to the desired vector.
        /// format: (x,y,e.t.c.)
        /// </summary>
        /// <param name="str"> the input string to parse into a vector </param>
        /// <returns>The parsed vector</returns>
        public static Vector3Int ParseVector3IntFromString(string str)
        {
            string[] temp = GetVectorInnerString(str).Split(',');
            return new Vector3Int(int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2]));
        }

        /// <summary>
        /// Parses a string so that it may be converted to the desired vector.
        /// format: (x,y,e.t.c.)
        /// </summary>
        /// <param name="str"> the input string to parse into a vector </param>
        /// <returns>The parsed vector</returns>
        public static Vector3 ParseVector3FromString(string str)
        {
            string[] temp = GetVectorInnerString(str).Split(',');
            return new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
        }

        /// <summary>
        /// Parses a string so that it may be converted to the desired vector.
        /// format: (x,y,e.t.c.)
        /// </summary>
        /// <param name="str"> the input string to parse into a vector </param>
        /// <returns>The parsed vector</returns>
        public static Vector4 ParseVector4FromString(string str)
        {
            string[] temp = GetVectorInnerString(str).Split(',');
            return new Vector4(
                float.Parse(temp[0]),
                float.Parse(temp[1]),
                float.Parse(temp[2]),
                float.Parse(temp[3])
            );
        }
        #endregion
    }
}
