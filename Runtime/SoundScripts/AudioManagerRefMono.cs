using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// A monobehavior wrapping audio manager play and stop commands for the convenience of UI access.
    /// Play Audio From Button e.t.c.
    /// </summary>
    public class AudioManagerRefMono : MonoBehaviour
    {
        public void PlaySoundCommand(string SoundName) => AudioManager.S.PlaySound(SoundName);
        public void StopSoundCommand(string SoundName) => AudioManager.S.StopSound(SoundName);
    }
}