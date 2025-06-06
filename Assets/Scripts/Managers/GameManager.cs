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
        public UniTask StartGame()
        {
            Debug.Log("Game Start");
            _screenManager.ShowScreen(GuiScreenIds.MainMenuScreen);
            return UniTask.CompletedTask;
        }

        public async UniTask Initialize()
        {
            Debug.Log("GameManager initialized");
            await _playerController.Initialize().ContinueWith(() =>
                  _loadManager.Load().ContinueWith(StartGame));
        }
    }
}