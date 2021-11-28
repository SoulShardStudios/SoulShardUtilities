using UnityEngine;
using System;
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
        [SerializeField] protected ChunkMapInt2D<T> _chunkmap = new ChunkMapInt2D<T>();
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
        protected int PPU;
        /// <summary>
        /// the callback for when a chunk is added, just ncase you want to do extra things when a chunk is added
        /// </summary>
        protected Action<T, Vector2Int> chunkAddCallback;
        /// <summary>
        /// adds a chunk to the chunkmap at the specified position
        /// </summary>
        /// <param name="chunkPosition"></param>
        public virtual void AddChunk(Vector2Int chunkPosition)
        {
            if (_chunkmap.chunks.ContainsKey(chunkPosition))
                return;
            Vector3 position = (Vector3)(chunkPosition * (int)_chunkmap.chunkSize + new Vector2(1, 1)) / PPU;
            GameObject G = Instantiate(_chunk, position, Quaternion.identity, _chunkTransformParent);
            G.name = chunkName + chunkPosition.ToString();
            T chunk = G.GetComponent<T>();
            _chunkmap.chunks.Add(chunkPosition, chunk);
            chunkAddCallback?.Invoke(chunk, chunkPosition);
        }
    }
}