using Cysharp.Threading.Tasks;
using Data.Load;
using Gameplay.Level;
using Gameplay.Player;
using GUI.Screens;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class GameManager : IGameManager
    {
        [Inject] private readonly IScreenManager _screenManager;
        [Inject] private readonly ILoadManager _loadManager;
        [Inject] private readonly IPlayerController _playerController;
        [Inject] private readonly ILevelManager _levelManager;

        public async UniTask Initialize()
        {
            await _loadManager.Initialize().ContinueWith(ShowMainMenu);
            //await _levelManager.Initialize(data);

//                .ContinueWith(ShowMainMenu);
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