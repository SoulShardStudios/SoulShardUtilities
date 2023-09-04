using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SoulShard.Utils
{
    public class AudioSourceManager : MonoBehaviour
    {
        [SerializeField]
        AudioMixerGroup _mixer;

        public SoundByte[] _sounds;

        List<(AudioSource, SoundByte)> _sources = new List<(AudioSource, SoundByte)>(0);

        protected virtual void Awake()
        {
            foreach (SoundByte soundByte in _sounds)
                if (soundByte.playOnAwake)
                    PlaySoundByte(soundByte);
        }

        public SoundByte ConvertNameToSound(string SoundName)
        {
            if (_sounds.Length == 0)
                return null;
            foreach (SoundByte S in _sounds)
                if (S.name.ToLower() == SoundName.ToLower() && S.name != "")
                    return S;
            return null;
        }

        protected AudioSource GetSourceFromQueue(SoundByte soundByte)
        {
            AudioSource source = null;
            for (var i = 0; i < _sources.Count; i++)
            {
                if (!_sources[i].Item1.isPlaying)
                {
                    source = _sources[i].Item1;
                    _sources[i] = (source, soundByte);
                }
            }
            if (source == null)
            {
                source = gameObject.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = _mixer;
                _sources.Add((source, soundByte));
            }
            return source;
        }

        public AudioSource PlaySoundByte(SoundByte sound)
        {
            if (sound == null)
                return null;
            var source = GetSourceFromQueue(sound);
            StartCoroutine(sound.Play(source, this));
            return source;
        }

        public AudioSource PlaySoundByte(string sound) => PlaySoundByte(ConvertNameToSound(sound));

        // this is so unity events can pick up this function. thanks for the duct tape due to the return type here.
        public void Play(string sound) => PlaySoundByte(sound);

        public void StopAllSoundBytes(string soundName)
        {
            foreach (var source in _sources)
            {
                if (source.Item2.name != soundName)
                    continue;
                if (!source.Item2.loop)
                    source.Item1.Stop();
                else
                    StartCoroutine(
                        SoundByte.Fade(false, source.Item2, source.Item1, source.Item1.volume, true)
                    );
            }
        }

        public bool IsPlayingByte(string soundName)
        {
            foreach (var source in _sources)
                if (source.Item2.name == soundName && source.Item1.isPlaying)
                    return true;
            return false;
        }
    }
}
