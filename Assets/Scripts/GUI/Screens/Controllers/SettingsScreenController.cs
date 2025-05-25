using GUI.Managers;
using GUI.Screens.Views;
using UnityEngine;
using VContainer;

namespace GUI.Screens.Controllers
{
    public class SettingsScreenController : IScreenController
    {
        private IGuiScreenManager _guiScreenManager;
        
        private SettingsScreenView _view;
        public string ID => GuiScreenIds.SettingsScreen;

        public void SetView(IScreenView view)
        {
            _view = view as SettingsScreenView;
        }

        public void Initialize(IGuiScreenManager guiScreenManager)
        {
            _guiScreenManager = guiScreenManager;
           Debug.Log("Initializing Settings Screen");
            _view.OnShow = RegisterListeners;
            _view.OnHidden = RemoveListeners;
        }

        private void RegisterListeners()
        {
            _view.MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            _guiScreenManager.ShowScreen(GuiScreenIds.MainMenuScreen);
        }

        private void RemoveListeners()
        {
            _view.MainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }
    }
}
