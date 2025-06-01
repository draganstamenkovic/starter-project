using System;
using GUI.Popups.Builder;

namespace GUI.Popups.Views
{
    public interface IPopupView
    {
        string ID { get; }
        Action OnShow { get; }
        Action OnShown { get; }
        Action OnHide { get; }
        Action OnHidden { get; }
        void Initialize();
        void Show(Action onComplete = null);
        void Hide(Action onComplete = null);
    }
}