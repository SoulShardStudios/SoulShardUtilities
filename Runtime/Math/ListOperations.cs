using System;
namespace SoulShard.Utils
{
    // this is just so that you can easily apply operations to an entire list without having to write a few lines of code each time
    // C# doesnt support generics for operators yet so this cannot sadly, be a generic
    public partial struct MathUtility
    {
        #region Int List Operators
        public static int[] AddToList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] += amount;
            return arr;
        }
        public static int[] SubtractFromList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] -= amount;
            return arr;
        }
        public static int[] MultiplyList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] *= amount;
            return arr;
        }
        public static int[] DivideList(int[] arr, int amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] /= amount;
            return arr;
        }
        public static int[] ApplyOperationToList(int[] arr, int amount, Func<int, int, int> @operator)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = @operator(arr[i], amount);
            return arr;
        }
        #endregion
        #region Float List Operators
        public static float[] AddToList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] += amount;
            return arr;
        }
        public static float[] SubtractFromList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] -= amount;
            return arr;
        }
        public static float[] MultiplyList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] *= amount;
            return arr;
        }
        public static float[] DivideList(float[] arr, float amount)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] /= amount;
            return arr;
        }
        public static float[] ApplyOperationToList(float[] arr, float amount, Func<float,float,float> @operator)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = @operator(arr[i],amount);
            return arr;
        }
        #endregion
    }
}
