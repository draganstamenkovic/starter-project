using Cysharp.Threading.Tasks;
using GUI.Screens;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class GameManager : IGameManager
    {
        [Inject] private readonly IScreenManager _screenManager;
        public UniTask StartGame()
        {
            Debug.Log("Game Start");
            _screenManager.ShowScreen(GuiScreenIds.MainMenuScreen);
            return UniTask.CompletedTask;
        }

        public async UniTask Initialize()
        {
            Debug.Log("GameManager initialized");
            await StartGame();
        }
    }
}