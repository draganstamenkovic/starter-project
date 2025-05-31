using System;
using Configs;
using DG.Tweening;
using GUI.Popups.Builder;
using UnityEngine;
using VContainer;

namespace GUI.Popups.Views
{
    public abstract class PopupView : MonoBehaviour, IPopupView
    {
        [Inject] private PopupsConfig _popupsConfig;
        
        [SerializeField] private CanvasGroup _canvasGroup;

        public virtual void Initialize(PopupData popupData)
        {
            _canvasGroup.alpha = 0;
        }

        public virtual void Show(Action onComplete = null)
        {
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, _popupsConfig.ShowTransitionDuration)
                .SetEase(_popupsConfig.TransitionEase).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        public virtual void Hide(Action onComplete = null)
        {
            _canvasGroup.DOFade(0f, _popupsConfig.HideTransitionDuration)
                .SetEase(_popupsConfig.TransitionEase).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }
    }
}