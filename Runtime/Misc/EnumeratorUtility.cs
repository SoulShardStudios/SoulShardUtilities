using System.Collections.Generic;

namespace SoulShard.Utils
{
    public static class EnumeratorUtility
    {
        public static IEnumerable<T> GetEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }

}
