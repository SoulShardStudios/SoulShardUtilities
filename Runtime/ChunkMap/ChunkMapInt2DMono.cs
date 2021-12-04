using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// this class is here to provide basic functionality to multiple chunk map dependant monobehaviours
    /// as most of them have some repeated variables, code, and initialization. 
    /// </summary>
    /// <typeparam name="T">the type of chunk that the chunk map should have</typeparam>
    public abstract class ChunkMapInt2DMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// the chunkmap which manages a good portion of functionality, and stores all of the chunks
        /// </summary>
        [SerializeField] public ChunkMapInt2D<T> chunkmap = new ChunkMapInt2D<T>();
        /// <summary>
        /// the transform parent of the instantiated chunks
        /// </summary>
        [SerializeField] protected Transform _chunkTransformParent;
        /// <summary>
        /// the chunk to instantiate
        /// </summary>
        [SerializeField] protected GameObject _chunk;
        /// <summary>
        /// the name to give the chunks when they are instantiated
        /// </summary>
        protected string chunkName;
        /// <summary>
        /// the pixels per unit of the environment. this is used for scaling based on pixels per unit for 2D games. 
        /// if you don't want to scale anything leave this at 1
        /// </summary>
        int _ppu;
        public int PPU 
        { 
            set 
            {
                value = value == 0 ? 1 : value;
                chunkmap.PPU = value;
                _ppu = value;
            }
            private get => _ppu;
        }
        /// <summary>
        /// Adds a chunk to the chunkmap
        /// </summary>
        /// <param name="chunkPosition">the chunk position to add the chunk at</param>
        /// <returns>a reference to the newly created chunk</returns>
        public virtual T AddChunk(Vector2Int chunkPosition)
        {
            if (chunkmap.chunks.ContainsKey(chunkPosition))
                return null;
            Vector3 position = (Vector3)(chunkPosition * (int)chunkmap.chunkSize + new Vector2(1, 1)) / _ppu;
            GameObject G = Instantiate(_chunk, position, Quaternion.identity, _chunkTransformParent);
            T chunk = G.GetComponent<T>();
            G.name = chunkName + chunkPosition.ToString();
            chunkmap.chunks.Add(chunkPosition, chunk);
            return chunk;
        }
    }
}