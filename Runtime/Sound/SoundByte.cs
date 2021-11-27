using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// ScriptableObject that contains the information to play a possibly random sound with custom pitch, volume, e.t.c.
    /// </summary>
    [CreateAssetMenu(menuName = "SoulShardUtils/Sounds/SoundByte")]
    public class SoundByte : ScriptableObject
    {
        [HideInInspector] public AudioSource Source;
        public string Name;
        public bool Loop, PlayOnAwake;
        public Sound[] possibleSounds;
    }
}