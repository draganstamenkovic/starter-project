using Gameplay;
using GUI.Screens.Views;
using UnityEngine;
using VContainer;

namespace GUI.Screens.Controllers
{
    public class PlayScreenController : IScreenController
    {
        [Inject] private IGameManager _gameManager;
        private IScreenManager _screenManager;
        
        private PlayScreenView _view;
        public string ID => GuiScreenIds.PlayScreen;

        public void SetView(IScreenView view)
        {
            _view = view as PlayScreenView;
        }

        public void Initialize(IScreenManager screenManager)
        {
            _screenManager = screenManager;
           Debug.Log("Initializing Play Screen");
            _view.OnShow = RegisterListeners;
            _view.OnShown = () =>
            {
                _gameManager.OnPlay?.Invoke();
            };
            _view.OnHide = () =>
            {
                _gameManager.OnStop?.Invoke();
            };
            _view.OnHidden = RemoveListeners;
        }

        private void RegisterListeners()
        {
            _view.MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            _screenManager.ShowScreen(GuiScreenIds.MainMenuScreen);
        }

        private void RemoveListeners()
        {
            _view.MainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }
    }
}
