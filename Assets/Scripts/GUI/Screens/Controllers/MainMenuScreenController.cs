using GUI.Managers;
using GUI.Screens.Views;
using UnityEngine;
using VContainer;

namespace GUI.Screens.Controllers
{
    public class MainMenuScreenController : IScreenController
    {
        private IGuiScreenManager _guiScreenManager;
        
        private MainMenuScreenView _view;
        public string ID => GuiScreenIds.MainMenuScreen;
        public void SetView(IScreenView view)
        {
            _view = view as MainMenuScreenView;
        }

        public void Initialize(IGuiScreenManager guiScreenManager)
        {
            Debug.Log("Initializing Main Menu Screen");
            _guiScreenManager = guiScreenManager;
            _view.OnShow = RegisterListeners;
            _view.OnHidden = RemoveListeners;
        }

        private void RegisterListeners()
        {
            _view.PlayButton.onClick.AddListener(OnPlayButtonClick);
            _view.SettingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnSettingsButtonClick()
        {
            _guiScreenManager.ShowScreen(GuiScreenIds.SettingsScreen);
        }

        private void OnPlayButtonClick()
        {
            _guiScreenManager.ShowScreen(GuiScreenIds.PlayScreen);
        }

        private void RemoveListeners()
        {
            _view.PlayButton.onClick.RemoveListener(OnPlayButtonClick);
            _view.SettingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        }
    }
}