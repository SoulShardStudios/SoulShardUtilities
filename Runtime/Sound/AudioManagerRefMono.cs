using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// A monobehavior wrapping audio manager play and stop commands for the convenience of UI access.
    /// Play Audio From Button e.t.c.
    /// </summary>
    public class AudioManagerRefMono : MonoBehaviour
    {
        /// <summary>
        /// play a sound with a given name
        /// </summary>
        /// <param name="SoundName">the name of the sound to play</param>
        public void PlaySoundCommand(string SoundName) => AudioManager.PlaySound(SoundName);

        /// <summary>
        /// stop a specific sound from playing
        /// </summary>
        /// <param name="SoundName">the name of the sound to stop playing</param>
        public void StopSoundCommand(string SoundName) => AudioManager.StopAllSounds(SoundName);
    }
}
