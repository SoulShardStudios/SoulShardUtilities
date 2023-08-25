using UnityEngine;
using System.ComponentModel;

namespace SoulShard.Utils
{
    /// <summary>
    /// stores a sound and some extra associated data for use in SoundBytes
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        [Range(0f, 1f)]
        [DefaultValue(1)]
        public float minVolume = 1;

        [Range(0f, 1f)]
        [DefaultValue(1)]
        public float maxVolume = 1;

        [Range(.1f, 3f)]
        [DefaultValue(1)]
        public float minPitch = 1;

        [Range(.1f, 3f)]
        [DefaultValue(1)]
        public float maxPitch = 1;

        public AudioClip audio;
    }
}
