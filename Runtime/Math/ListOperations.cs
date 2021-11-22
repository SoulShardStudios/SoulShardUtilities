using System;
namespace SoulShard.Math
{
    // this is just so that you can easily apply operations to an entire list without having to write a few lines of code each time
    // C# doesnt support generics for operators yet so this cannot sadly, be a generic
    public partial struct MathUtility
    {
        #region Int List Operators
        /// <summary>
        /// adds a value to an entire list
        /// </summary>
        /// <param name="arr">the list to add the value to</param>
        /// <param name="amount">the amount to add</param>
        /// <returns>the modified collection</returns>
        public static int[] AddToList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] += amount;
            return arr;
        }
        /// <summary>
        /// subtracts a value to an entire list
        /// </summary>
        /// <param name="arr">the list to subtract the value from</param>
        /// <param name="amount">the amount to subtract</param>
        /// <returns>the modified collection</returns>
        public static int[] SubtractFromList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] -= amount;
            return arr;
        }
        /// <summary>
        /// multiplies a value to an entire list
        /// </summary>
        /// <param name="arr">the list to multiply all the values of</param>
        /// <param name="amount">the amount to multiply</param>
        /// <returns>the modified collection</returns>
        public static int[] MultiplyList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] *= amount;
            return arr;
        }
        /// <summary>
        /// divides a value to an entire list
        /// </summary>
        /// <param name="arr">the list to divide all the values of</param>
        /// <param name="amount">the amount to subtract</param>
        /// <returns>the modified collection</returns>
        public static int[] DivideList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] /= amount;
            return arr;
        }
        /// <summary>
        /// applies a given operation to all values in a list
        /// </summary>
        /// <param name="arr">the array of values to apply the operation to</param>
        /// <param name="amount">the amount given to the operation</param>
        /// <param name="operator">the operation</param>
        /// <returns>the modified collection</returns>
        public static int[] ApplyOperationToList(int[] arr, int amount, Func<int, int, int> @operator)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = @operator(arr[i], amount);
            return arr;
        }
        #endregion
        #region Float List Operators
        /// <summary>
        /// adds a value to an entire list
        /// </summary>
        /// <param name="arr">the list to add the value to</param>
        /// <param name="amount">the amount to add</param>
        /// <returns>the modified collection</returns>
        public static float[] AddToList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] += amount;
            return arr;
        }
        /// <summary>
        /// subtracts a value to an entire list
        /// </summary>
        /// <param name="arr">the list to subtract the value from</param>
        /// <param name="amount">the amount to subtract</param>
        /// <returns>the modified collection</returns>
        public static float[] SubtractFromList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] -= amount;
            return arr;
        }
        /// <summary>
        /// multiplies a value to an entire list
        /// </summary>
        /// <param name="arr">the list to multiply all the values of</param>
        /// <param name="amount">the amount to multiply</param>
        /// <returns>the modified collection</returns>
        public static float[] MultiplyList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] *= amount;
            return arr;
        }
        /// <summary>
        /// divides a value to an entire list
        /// </summary>
        /// <param name="arr">the list to divide all the values of</param>
        /// <param name="amount">the amount to subtract</param>
        /// <returns>the modified collection</returns>
        public static float[] DivideList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] /= amount;
            return arr;
        }
        /// <summary>
        /// applies a given operation to all values in a list
        /// </summary>
        /// <param name="arr">the array of values to apply the operation to</param>
        /// <param name="amount">the amount given to the operation</param>
        /// <param name="operator">the operation</param>
        /// <returns>the modified collection</returns>
        public static float[] ApplyOperationToList(float[] arr, float amount, Func<float,float,float> @operator)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = @operator(arr[i],amount);
            return arr;
        }
        #endregion
    }
}
