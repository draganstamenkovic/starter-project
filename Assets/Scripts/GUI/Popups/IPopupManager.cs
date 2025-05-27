using System;
using Cysharp.Threading.Tasks;
using GUI.Popups.Builder;
using UnityEngine;

namespace GUI.Popups
{
    public interface IPopupManager
    {
        UniTask Initialize(Transform parent, GameObject screenBlocker);
        void ShowConfirmationPopup(PopupData popupDataData, Action callback = null);
        void HideConfirmationPopup(Action callback = null);
        void ShowPopupScreen(string id, Action callback);
        void HidePopupScreen(string id, Action callback);
        
    }
}