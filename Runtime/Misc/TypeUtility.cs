namespace SoulShard.Utils
{
    /// <summary>
    /// holds methods for some special type conversions
    /// </summary>
    public struct TypeUtility
    {
        public static bool TryChangeTypeIFormattable<T, TR>(T input, out TR output) where T : System.IFormattable 
        {
            bool result = false;
            try
            {
                System.Type type = System.Nullable.GetUnderlyingType(typeof(TR));
                output = (TR)System.Convert.ChangeType(input, type);
                result = true;
            }
            catch (System.Exception)
            {
                output = default(TR);
            }
            return result;
        }
        public static bool TryChangeTypeIConvertible<T, TR>(T input, out TR output) where T : System.IConvertible
        {
            bool result = false;
            try
            {
                System.Type type = System.Nullable.GetUnderlyingType(typeof(TR));
                output = (TR)System.Convert.ChangeType(input, type);
                result = true;
            }
            catch (System.Exception)
            {
                output = default(TR);
            }
            return result;
        }
    }
}