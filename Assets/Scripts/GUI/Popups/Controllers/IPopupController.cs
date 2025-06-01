using GUI.Popups.Views;

namespace GUI.Popups.Controllers
{
    public interface IPopupController
    {
        string ID { get; }
        void SetView(IPopupView view);
        void Initialize(IPopupManager popupManager);
    }
}