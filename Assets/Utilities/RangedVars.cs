// used for instances where a single generic T needs to have a minimum and maximum value. EX: a minimum and a maximum value for an int to have
namespace SoulShard.Utils
{
    [System.Serializable]
    public class Ranged<T>
    {
        // a range of values from the min to the max. 
        public T Min, Max;
        public Ranged(T min, T max)
        {
            Min = min;
            Max = max;
        }
        public Ranged() { }
    }
}