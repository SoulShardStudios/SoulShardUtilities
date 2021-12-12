using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// since many chunk maps have some sort of externally controled initialization this class was created. it does the job of singleinitmono and chunkmapint2d mono
    /// </summary>
    /// <typeparam name="T">the type of chunk that the chunk map should have</typeparam>
    public class ChunkMapInt2DMonoWithSingleInit<T> : ChunkMapInt2DMono<T> where T : MonoBehaviour
    {
        /// <summary>
        /// whether the Monobehavior has been initialized.
        /// </summary>
        [HideInInspector] public bool initialized;
        /// <summary>
        /// whether this Monobehavior is initialized externally
        /// </summary>
        public bool initializedExternally;
        protected virtual void OnEnable()
        {
            if (!initializedExternally)
                Init();
        }
        public virtual void Init() { }
    }
}