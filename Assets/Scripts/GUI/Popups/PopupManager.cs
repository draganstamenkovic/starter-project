using System;
using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using GUI.Popups.Builder;
using GUI.Popups.Controllers;
using GUI.Popups.Views;
using GUI.Screens.Views;
using VContainer.Unity;

namespace GUI.Popups
{
    public class PopupManager : IPopupManager
    {
        [Inject] private readonly PopupsConfig _popupConfig;
        [Inject] private IEnumerable<IPopupController> _controllers;
        
        private IObjectResolver _objectResolver;
        private readonly Dictionary<string, RectTransform> _popups = new();
        private readonly Dictionary<string, RectTransform> _spawnedPopups = new();
        private readonly Dictionary<string, IPopupView> _spawnedPopupViews = new();
        
        private Transform _popupParent;
        private GameObject _screenBlocker;

        public PopupManager(IObjectResolver resolver)
        {
            _objectResolver = resolver;
        }

        public async UniTask Initialize(Transform parent, GameObject screenBlocker)
        {
            _popupParent = parent;
            _screenBlocker = screenBlocker;
            
            foreach (var popup in _popupConfig.Popups)
            {
                if (!_popups.ContainsKey(popup.Name))
                {
                    _popups.Add(popup.Name, popup.PopupPrefab);
                }
            }

            await UniTask.CompletedTask;
        }

        public void ShowConfirmationPopup(PopupData popupData, Action callback = null)
        {
            _screenBlocker.SetActive(true);
            var popup = GetPopupRectTransform();
            
            var popupView = popup.GetComponent<ConfirmationPopupView>();
            popupView.SetData(popupData);
            
            popupView.Show(() =>
            {
                _screenBlocker.SetActive(false);
                callback?.Invoke();
            });
        }

        public void HideConfirmationPopup(Action callback = null)
        {
            if (_spawnedPopupViews.TryGetValue(PopupIds.ConfirmationPopup, out var popupView))
            {
                _screenBlocker.SetActive(true);
                popupView.Hide(() =>
                {
                    _screenBlocker.SetActive(false);
                    callback?.Invoke();
                });
            }
        }

        public void ShowPopupScreen(string id, Action callback)
        {
            throw new NotImplementedException();
        }

        public void HidePopupScreen(string id, Action callback)
        {
            throw new NotImplementedException();
        }

        private RectTransform GetPopupRectTransform()
        {
            if (_spawnedPopups.TryGetValue(PopupIds.ConfirmationPopup, out var spawnedPopup))
            {
                return spawnedPopup;
            }

            if (!_popups.TryGetValue(PopupIds.ConfirmationPopup, out var popup))
            {
                Debug.LogError("Confirmation popup is not added in config");
            }

            var popupRect = _objectResolver.Instantiate(popup, _popupParent);
            _spawnedPopups.Add(PopupIds.ConfirmationPopup, popupRect);
            _spawnedPopupViews.Add(PopupIds.ConfirmationPopup, popupRect.GetComponent<IPopupView>());
            
            InitializePopup(popupRect);
            
            return popupRect;
        }

        private void InitializePopup(RectTransform popupRect)
        {
            var view = popupRect.GetComponent<IPopupView>();
            if (view == null)
            {
                Debug.LogError("Popup view is not added to popup GameObject");
            }
            else
            {
                view.Initialize();
                foreach (var controller in _controllers)
                {
                    if (controller.ID.Equals(view.ID))
                    {
                        controller.SetView(view);
                        controller.Initialize(this);
                        break;
                    }
                }
            }
        }
    }
}