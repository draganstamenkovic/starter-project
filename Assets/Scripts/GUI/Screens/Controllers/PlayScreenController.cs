using Gameplay.Player;
using GUI.Screens.Views;
using UnityEngine;
using VContainer;

namespace GUI.Screens.Controllers
{
    public class PlayScreenController : IScreenController
    {
        [Inject] private IPlayerController _playerController;
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
                _playerController.SetActive(true);
            };
            _view.OnHide = () =>
            {
                _playerController.SetActive(false);
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
