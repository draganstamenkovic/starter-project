using GUI.Popups;
using GUI.Popups.Builder;
using GUI.Screens.Views;
using UnityEngine;
using VContainer;

namespace GUI.Screens.Controllers
{
    public class SettingsScreenController : IScreenController
    {
        private IScreenManager _screenManager;
        
        private SettingsScreenView _view;
        [Inject] private IPopupBuilder _popupBuilder;
        [Inject] private IPopupManager _popupManager;
        public string ID => GuiScreenIds.SettingsScreen;

        public void SetView(IScreenView view)
        {
            _view = view as SettingsScreenView;
        }

        public void Initialize(IScreenManager screenManager)
        {
            _screenManager = screenManager;
           Debug.Log("Initializing Settings Screen");
            _view.OnShow = RegisterListeners;
            _view.OnHidden = RemoveListeners;
        }

        private void RegisterListeners()
        {
            _view.MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _view.OneButtonPopup.onClick.AddListener(OnShowPopupWithOneButton);
            _view.TwoButtonPopup.onClick.AddListener(OnShowPopupWithTwoButton);
        }

        private void OnShowPopupWithTwoButton()
        {
            var popupData = _popupBuilder.Title("Two buttons popup")
                .Text("This is an example popup with two buttons")
                .AddButton("Yes", Color.green, () => { Debug.Log("Yes clicked!");})
                .AddButton("No", Color.white, () => { _popupManager.HideConfirmationPopup(); })
                .Build();
            
            _popupManager.ShowConfirmationPopup(popupData);
        }

        private void OnMainMenuButtonClicked()
        {
            _screenManager.ShowScreen(GuiScreenIds.MainMenuScreen);
        }

        private void RemoveListeners()
        {
            _view.MainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _view.OneButtonPopup.onClick.RemoveListener(OnShowPopupWithOneButton);
            _view.TwoButtonPopup.onClick.RemoveListener(OnShowPopupWithTwoButton);
        }

        private void OnShowPopupWithOneButton()
        {
            var popupData = _popupBuilder.Title("One button popup")
                .Text("This is an example popup with one button")
                .AddButton("Ok", Color.green, () =>
                {
                    _popupManager.HideConfirmationPopup();
                })
                .Build();
            
            _popupManager.ShowConfirmationPopup(popupData);
        }
    }
}
