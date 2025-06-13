using System;
using Cysharp.Threading.Tasks;
using Data.Load;
using Gameplay.Level;
using Gameplay.Player;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class GameManager : IGameManager
    {
        [Inject] private readonly ILoadManager _loadManager;
        [Inject] private readonly IPlayerController _playerController;
        [Inject] private readonly ILevelManager _levelManager;

        public Action OnPause { get; set; }
        public Action OnResume { get; set; }
        public Action OnPlay { get; set; }
        public Action OnStop { get; set; }

        public async UniTask Initialize()
        {
            await _loadManager.Initialize().ContinueWith(() =>
                _playerController.Initialize()).ContinueWith(() =>
                _levelManager.Initialize()).ContinueWith(SubscribeToEvents);
        }

        public void Play()
        {
            Debug.Log("Play");
            var gameplayRoot = new GameObject("_Gameplay_");
        }

        public void Pause()
        {
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            Time.timeScale = 1f;
        }

        public void Stop()
        {
            
        }

        public void Quit()
        {
#if UNITY_ANDROID || UNITY_IOS
            Application.Quit();
#else
            Debug.Log("Quit called");
#endif
        }

        private void SubscribeToEvents()
        {
            OnPause = Pause;
            OnResume = Resume;
            OnPlay = Play;
            OnStop = Stop;
        }
    }
}