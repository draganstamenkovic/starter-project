using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip gameMusic;
        public AudioClip shotSound;
        public AudioClip explosionSound;
    }
}