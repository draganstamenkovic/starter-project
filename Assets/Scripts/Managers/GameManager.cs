using Cysharp.Threading.Tasks;
using GUI.Managers;
using GUI.Screens;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class GameManager : IGameManager
    {
        [Inject] private readonly IGuiScreenManager _guiScreenManager;
        public UniTask StartGame()
        {
            Debug.Log("Game Start");
            _guiScreenManager.ShowScreen(GuiScreenIds.MainMenuScreen);
            return UniTask.CompletedTask;
        }

        public async UniTask Initialize()
        {
            Debug.Log("GameManager initialized");
            await StartGame();
        }
    }
}