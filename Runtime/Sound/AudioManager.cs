using UnityEngine;

namespace SoulShard.Utils
{
    public class AudioManager : AudioSourceManager
    {
        static AudioManager _instance;

        protected override void Awake() => _instance = this;

        public static SoundByte ConvertSoundNameToSound(string SoundName) =>
            _instance.ConvertNameToSound(SoundName);

        public static AudioSource PlaySound(string soundName) => _instance.PlaySoundByte(soundName);

        public static void StopAllSounds(string soundName) =>
            _instance.StopAllSoundBytes(soundName);

        public static bool IsPlaying(string soundName) => _instance.IsPlayingByte(soundName);
    }
}
