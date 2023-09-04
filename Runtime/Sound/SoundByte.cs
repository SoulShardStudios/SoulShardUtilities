using UnityEngine;
using System.Collections;

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

        public IEnumerator Play(AudioSource source, MonoBehaviour mono)
        {
            Sound soundClip = possibleSounds.RandomElement();
            yield return new WaitForSeconds(soundClip.GetStartDelay());
            source.clip = soundClip.audio;
            source.pitch = soundClip.GetPitch();
            source.playOnAwake = playOnAwake;
            source.loop = loop;
            source.spatialBlend = soundClip.GetSpatialBlend();
            source.rolloffMode = soundClip.rolloff;
            source.minDistance = soundClip.minDistance;
            source.maxDistance = soundClip.maxDistance;
            source.dopplerLevel = soundClip.dopplerLevel;
            var volume = soundClip.GetVolume();
            source.volume = 0;
            if (fadeInDuration != 0)
                mono.StartCoroutine(Fade(true, this, source, volume, false));
            if (fadeOutDuration != 0 && !loop)
                mono.StartCoroutine(Fade(false, this, source, volume, false));
            if (fadeInDuration == 0 && fadeOutDuration == 0)
                source.volume = volume;
            source.Play();
        }

        // See https://www.youtube.com/watch?v=kYGXGDjL5jM
        // yes I am a goblin for this but IDC!!! unity should have this!!!
        internal static IEnumerator Fade(
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
    }
}
