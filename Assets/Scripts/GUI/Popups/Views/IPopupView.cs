using System;
using GUI.Popups.Builder;

namespace GUI.Popups.Views
{
    public interface IPopupView
    {
        void Initialize(PopupData popupData);
        void Show(Action onComplete = null);
        void Hide(Action onComplete = null);
    }
}