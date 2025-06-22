using Cysharp.Threading.Tasks;
using Data.Load;
using Gameplay.Level;
using Gameplay.Player;
using Input;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class GameManager : IGameManager
    {
        [Inject] private readonly InputManager _inputManager;
        [Inject] private readonly ILoadManager _loadManager;
        [Inject] private readonly IPlayerController _playerController;
        [Inject] private readonly ILevelManager _levelManager;

        private Transform _gameplayParent;
        private bool _isPaused;

        public async UniTask Initialize()
        {
            CreateGameplayParent();
            _loadManager.Initialize();
            _playerController.Initialize(_gameplayParent);
            _inputManager.Initialize(_playerController);
            _levelManager.Initialize();
            await UniTask.CompletedTask;
        }

        public void Play()
        {
            _playerController.SetActive(true);
            _inputManager.SetActive(true);
        }

        public void Pause()
        {
            _isPaused = true;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            if (!_isPaused) return;
            
            _isPaused = false;
            Time.timeScale = 1f;
        }

        public void Stop()
        {
            if (_isPaused)
            {
                Time.timeScale = 1f;
                _isPaused = false;
            }
            _playerController.SetActive(false);
            _inputManager.SetActive(false);
        }

        public void Quit()
        {
            Stop();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID || UNITY_IOS
            Application.Quit();
#endif
        }
        private void CreateGameplayParent()
        {
            var gameplayObj = new GameObject("_Gameplay_");
            _gameplayParent = gameplayObj.transform;
        }
    }
}