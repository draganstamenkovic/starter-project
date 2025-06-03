using GUI.Popups.Views;
using UnityEngine;

namespace GUI.Popups.Controllers
{
    public class ShopPopupController : IPopupController
    {
        private IPopupManager _popupManager;
        private ShopPopupView _view;
        public string ID => PopupIds.ShopPopup;

        public void SetView(IPopupView view)
        {
            _view = view as ShopPopupView;
        }

        public void Initialize(IPopupManager popupManager)
        {
           Debug.Log("Initializing Shop Popup");
            _popupManager = popupManager;
            _view.OnShow = RegisterListeners;
            _view.OnHidden = RemoveListeners;
        }

        private void RegisterListeners()
        {
            _view.BackgroundButton.onClick.AddListener(HidePopup);
            _view.BuyButton.onClick.AddListener(OnBuyButtonClicked);
            _view.CloseButton.onClick.AddListener(HidePopup);
        }

        private void RemoveListeners()
        {
            _view.BackgroundButton.onClick.RemoveListener(HidePopup);
            _view.BuyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _view.CloseButton.onClick.RemoveListener(HidePopup);
        }

        private void HidePopup()
        {
            _popupManager.HidePopup(PopupIds.ShopPopup);
        }
        private void OnBuyButtonClicked()
        {
            Debug.Log("OnBuyButtonClicked");
        }
    }
}
