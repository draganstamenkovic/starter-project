using Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Audio.Managers
{
    public class AudioManager : IAudioManager
    {
        [Inject] private AudioConfig _audioConfig;
        private AudioSource _audioSource;
        public async UniTask Initialize()
        {
            var audioManager = new GameObject("_AudioManager_");
            audioManager.AddComponent<AudioSource>();
            _audioSource = audioManager.GetComponent<AudioSource>();
            
            await UniTask.CompletedTask;
        }

        public void Play()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }
    }
}