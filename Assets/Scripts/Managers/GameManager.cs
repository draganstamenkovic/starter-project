using Cysharp.Threading.Tasks;
using Data.Load;
using GUI.Screens;
using Player;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class GameManager : IGameManager
    {
        [Inject] private readonly IScreenManager _screenManager;
        [Inject] private readonly ILoadManager _loadManager;
        [Inject] private readonly IPlayerController _playerController;

        public async UniTask Initialize()
        {
            await _loadManager.Initialize()
                .ContinueWith(ShowMainMenu);
        }

        public void Play()
        {
            Debug.Log("Play");
            var gameplayRoot = new GameObject("_Gameplay_");
            
        }

        public void Pause()
        {
            _screenManager.ShowScreen(GuiScreenIds.SettingsScreen)
                .ContinueWith(() =>
                {
                    Time.timeScale = 0f;
                });
        }

        public void Resume()
        {
            _screenManager.HideScreen(GuiScreenIds.SettingsScreen)
                .ContinueWith(() =>
                {
                    Time.timeScale = 1f;
                });
        }

        public void Quit()
        {
#if UNITY_ANDROID || UNITY_IOS
            Application.Quit();
#else
            Debug.Log("Quit called");
#endif
        }

        public async UniTask ShowMainMenu()
        {
            Debug.Log("Game Start");
            await _screenManager.ShowScreen(GuiScreenIds.MainMenuScreen);
        }
    }
}