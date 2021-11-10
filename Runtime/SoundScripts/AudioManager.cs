using UnityEngine;
using UnityEngine.Audio;
namespace SoulShard.Utils
{
    /// <summary>
    /// A singleton responsible for allowing any script to play or stop specfic SoundBytes.
    /// Doesn't allow multiple SoundBytes of the same type to be played at the same time
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        #region Vars
        [SerializeField] AudioMixerGroup _mixer;
        /// <summary>
        /// the singleton value for this class
        /// </summary>
        public static AudioManager S { get; private set; }
        public SoundByte[] _sounds;
        #endregion
        #region Methods
        void Awake()
        {
            S = this;
            foreach (SoundByte S in _sounds)
                InitializeAudioClip(S);
        }
        /// <summary>
        /// converts a name to a given SoundByte stored in the AudioManagers
        /// </summary>
        /// <param name="SoundName">the name of the sound</param>
        /// <returns>the associated SoundByte</returns>
        public SoundByte ConvertSoundNameToSound(string SoundName)
        {
            if (_sounds.Length == 0)
                return null;
            foreach (SoundByte S in _sounds)
                if (S.Name.ToLower() == SoundName.ToLower() && S.name != "")
                    return S;
            return null;
        }
        #region AudioClip Management Methods
        void InitializeAudioClip(SoundByte sound)
        {
            if (sound.possibleSounds.Length == 0)
                sound.name = "";
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.outputAudioMixerGroup = _mixer;
            sound.Source.playOnAwake = sound.PlayOnAwake;
            sound.Source.loop = sound.Loop;
            RandomizeAudioClip(sound);
            if (sound.PlayOnAwake)
                sound.Source.Play();
        }
        void RandomizeAudioClip(SoundByte sound)
        {
            if (sound == null)
                return;
            int index = Random.Range(0, sound.possibleSounds.Length);
            sound.Source.clip = sound.possibleSounds[index].audio;
            sound.Source.volume = sound.possibleSounds[index].Volume;
            sound.Source.pitch = sound.possibleSounds[index].Pitch;
        }
        #endregion
        #region Sound Playing methods
        /// <summary>
        /// Play a specific sound
        /// </summary>
        /// <param name="sound">the SoundByte to play</param>
        public void PlaySound(SoundByte sound)
        {
            RandomizeAudioClip(sound);
            sound?.Source.Play();
        }
        /// <summary>
        /// stop playing a specific sound if its currently playing
        /// </summary>
        /// <param name="sound">the SoundByte to stop playing</param>
        public void StopSound(SoundByte sound) => sound?.Source.Stop();
        /// <summary>
        /// stop playing a specific sound if its currently playing
        /// </summary>
        /// <param name="SoundName">the name of the sound to stop playing</param>
        public void StopSound(string SoundName) => StopSound(ConvertSoundNameToSound(SoundName));
        /// <summary>
        /// Play a specific sound
        /// </summary>
        /// <param name="SoundName">the name of the sound to play</param>
        public void PlaySound(string SoundName) => PlaySound(ConvertSoundNameToSound(SoundName));
        #endregion
        #endregion
    }
}