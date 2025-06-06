using GUI.Popups;
using GUI.Screens.Views;
using UnityEngine;
using VContainer;

namespace GUI.Screens.Controllers
{
    public class MainMenuScreenController : IScreenController
    {
        [Inject] private IPopupManager _popupManager;
        private IScreenManager _screenManager;
        
        private MainMenuScreenView _view;
        public string ID => GuiScreenIds.MainMenuScreen;
        public void SetView(IScreenView view)
        {
            _view = view as MainMenuScreenView;
        }

        public void Initialize(IScreenManager screenManager)
        {
            Debug.Log("Initializing Main Menu Screen");
            _screenManager = screenManager;
            _view.OnShow = RegisterListeners;
            _view.OnShown = ShowOffer;
            _view.OnHidden = RemoveListeners;
        }

        private void ShowOffer()
        {
            _popupManager.ShowPopupScreen(PopupIds.ShopPopup);
        }

        private void RegisterListeners()
        {
            _view.PlayButton.onClick.AddListener(OnPlayButtonClick);
            _view.SettingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnSettingsButtonClick()
        {
            _screenManager.ShowScreen(GuiScreenIds.SettingsScreen);
        }

        private void OnPlayButtonClick()
        {
            _screenManager.ShowScreen(GuiScreenIds.PlayScreen);
        }

        private void RemoveListeners()
        {
            _view.PlayButton.onClick.RemoveListener(OnPlayButtonClick);
            _view.SettingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        }
    }
}