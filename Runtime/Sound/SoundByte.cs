using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// ScriptableObject that contains the information to play a possibly random sound with custom pitch, volume, e.t.c.
    /// </summary>
    [CreateAssetMenu(menuName = "SoulShardUtils/Sounds/SoundByte")]
    public class SoundByte : ScriptableObject
    {
        public bool loop,
            playOnAwake = false;
        public float fadeInDuration = 0;
        public float fadeOutDuration = 0;
        public Sound[] possibleSounds;
    }
}
