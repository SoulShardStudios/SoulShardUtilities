using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// stores a sound and some extra associated data for use in SoundBytes
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        [Range(0f, 1f)]
        public float minVolume = 1;

        [Range(0f, 1f)]
        public float maxVolume = 1;

        [Range(.1f, 3f)]
        public float minPitch = 1;

        [Range(.1f, 3f)]
        public float maxPitch = 1;

        public float minStartDelay = 0;

        public float maxStartDelay = 0;

        [Range(0f, 1f)]
        public float minSpatialBlend = 0;

        [Range(0f, 1f)]
        public float maxSpatialBlend = 0;

        public float GetStartDelay() => new Vector2(minStartDelay, maxStartDelay).RandomBetween();

        public float GetPitch() => new Vector2(minPitch, maxPitch).RandomBetween();

        public float GetVolume() => new Vector2(minVolume, maxVolume).RandomBetween();

        public float GetSpatialBlend() =>
            new Vector2(minSpatialBlend, maxSpatialBlend).RandomBetween();

        public AudioRolloffMode rolloff;
        public float minDistance;
        public float maxDistance;

        public AudioClip audio;
    }
}
