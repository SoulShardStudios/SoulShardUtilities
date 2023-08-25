using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SoulShard.Utils
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        AudioMixerGroup _mixer;

        static AudioManager _instance;
        public SoundByte[] _sounds;

        List<(AudioSource, SoundByte)> _sources = new List<(AudioSource, SoundByte)>(0);

        void Awake()
        {
            _instance = this;
            foreach (SoundByte soundByte in _sounds)
                if (soundByte.playOnAwake)
                    StartAudioClip(soundByte).Play();
        }

        public static SoundByte ConvertSoundNameToSound(string SoundName)
        {
            if (_instance._sounds.Length == 0)
                return null;
            foreach (SoundByte S in _instance._sounds)
                if (S.name.ToLower() == SoundName.ToLower() && S.name != "")
                    return S;
            return null;
        }

        AudioSource GetSourceFromQueue(SoundByte soundByte)
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

        AudioSource StartAudioClip(SoundByte soundByte)
        {
            if (soundByte == null)
                return null;
            var source = GetSourceFromQueue(soundByte);
            Sound soundClip = soundByte.possibleSounds.RandomElement();
            source.clip = soundClip.audio;
            source.pitch = Random.Range(soundClip.minPitch, soundClip.maxPitch);
            source.playOnAwake = soundByte.playOnAwake;
            source.loop = soundByte.loop;
            var volume = Random.Range(soundClip.minVolume, soundClip.maxVolume);
            source.volume = 0;
            if (soundByte.fadeInDuration != 0)
                StartCoroutine(Fade(true, soundByte, source, volume, false));
            if (soundByte.fadeOutDuration != 0 && !soundByte.loop)
                StartCoroutine(Fade(false, soundByte, source, volume, false));
            if (soundByte.fadeInDuration == 0 && soundByte.fadeOutDuration == 0)
                source.volume = volume;
            return source;
        }

        // See https://www.youtube.com/watch?v=kYGXGDjL5jM
        // yes I am a goblin for this but IDC!!! unity should have this!!!
        IEnumerator Fade(
            bool fadeIn,
            SoundByte soundByte,
            AudioSource source,
            float mainClipVolume,
            bool stopOnComplete
        )
        {
            var duration = fadeIn ? soundByte.fadeInDuration : soundByte.fadeOutDuration;
            if (!fadeIn)
            {
                double lengthOfSource = (double)source.clip.samples / source.clip.frequency;
                yield return new WaitForSeconds((float)(lengthOfSource - duration));
            }
            var time = 0f;
            var targetVolume = fadeIn ? mainClipVolume : 0f;
            var startVolume = fadeIn ? 0f : mainClipVolume;
            while (time < duration)
            {
                time += Time.deltaTime;
                source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
                yield return null;
            }
            if (stopOnComplete)
                source.Stop();
            yield break;
        }

        public static AudioSource PlaySound(string soundName)
        {
            var source = _instance.StartAudioClip(ConvertSoundNameToSound(soundName));
            source.Play();
            return source;
        }

        public static void StopAllSounds(string soundName)
        {
            foreach (var source in _instance._sources)
            {
                if (source.Item2.name != soundName)
                    continue;
                if (!source.Item2.loop)
                    source.Item1.Stop();
                else
                    _instance.StartCoroutine(
                        _instance.Fade(false, source.Item2, source.Item1, source.Item1.volume, true)
                    );
            }
        }
    }
}
