using Gameplay;
using GUI.Popups.Views;
using UnityEngine;
using VContainer;

namespace GUI.Popups.Controllers
{
    public class LevelFinishedPopupController : IPopupController
    {
        [Inject] private readonly IEventBus _eventBus;
        private IPopupManager _popupManager;
        private LevelFinishedPopupView _view;
        public string ID => PopupIds.LevelFinishedPopup;

        public void SetView(IPopupView view)
        {
            _view = view as LevelFinishedPopupView;
        }

        public void Initialize(IPopupManager popupManager)
        {
           Debug.Log("Initializing LevelFinished Popup");
            _popupManager = popupManager;
            _view.OnShow = RegisterListeners;
            _view.OnHidden = RemoveListeners;
        }

        private void RegisterListeners()
        {
            _view.QuitButton.onClick.AddListener(OnQuitButtonClicked);
            _view.ContinueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void OnContinueButtonClicked()
        {
            _popupManager.HidePopup(PopupIds.LevelFinishedPopup);
            _eventBus.Raise(EventType.LoadNextLevel);
        }

        private void OnQuitButtonClicked()
        {
            _popupManager.HidePopup(PopupIds.LevelFinishedPopup);
            _eventBus.Raise(EventType.ShowMainMenuScreen);
        }

        private void RemoveListeners()
        {
            _view.QuitButton.onClick.RemoveListener(OnQuitButtonClicked);
            _view.ContinueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }
    }
}
