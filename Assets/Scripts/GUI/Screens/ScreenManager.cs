using System;
using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GUI.Screens.Controllers;
using GUI.Screens.Views;
using Message;
using Message.Messages;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GUI.Screens
{
    public class ScreenManager : IScreenManager
    {
        [Inject] private GUIScreensConfig _guiScreensConfig;
        [Inject] private IEnumerable<IScreenController> _controllers;
        [Inject] private readonly IMessageBroker _messageBroker;

        private readonly IObjectResolver _objectResolver;
        private readonly Dictionary<string, RectTransform> _screens = new();
        private readonly Dictionary<string, RectTransform> _spawnedScreens = new();

        private IDisposable _disposableMessage;
        
        private GameObject _screenBlocker;
        private Transform _screenParent;
        private string _currentScreen;

        public ScreenManager(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public void Initialize(Transform parent, GameObject screenBlocker)
        {
            _screenParent = parent;
            _screenBlocker = screenBlocker;
            _disposableMessage = _messageBroker.Receive<ShowScreenMessage>().Subscribe(message =>
            {
                ShowScreen(GuiScreenIds.MainMenuScreen);
            });
            
            foreach (var screen in _guiScreensConfig.Screens)
            {
                if (!_screens.ContainsKey(screen.Name))
                {
                    _screens.Add(screen.Name, screen.Screen);
                }
            }
        }

        public async UniTask ShowScreen(string screenName)
        {
            _screenBlocker.SetActive(true);
            if (!string.IsNullOrEmpty(_currentScreen))
            {
                await HideScreen(_currentScreen);
            }

            _currentScreen = screenName;

            var screen = GetOrCreateScreen(screenName);
            var view = screen.GetComponent<IScreenView>();

            view.OnShow?.Invoke();

            await MoveScreen(screen, TransitionDirection.Center).ContinueWith(() =>
            {
                view.OnShown?.Invoke();
                _screenBlocker.SetActive(false);
            });
        }

        public async UniTask HideScreen(string screenName)
        {
            if (!_spawnedScreens.TryGetValue(screenName, out var screen))
            {
                Debug.LogWarning($"Trying to hide unspawned screen: {screenName}");
                return;
            }

            var view = screen.GetComponent<IScreenView>();
            view.OnHide?.Invoke();

            await MoveScreen(screen, TransitionDirection.Forward).ContinueWith(() =>
            {
                screen.gameObject.SetActive(false);
                view.OnHidden?.Invoke();
            });
        }

        private RectTransform GetOrCreateScreen(string screenName)
        {
            if (_spawnedScreens.TryGetValue(screenName, out var spawnedScreen))
            {
                spawnedScreen.gameObject.SetActive(true);
                return spawnedScreen;
            }

            if (!_screens.TryGetValue(screenName, out var screenPrefab))
            {
                Debug.LogError($"Screen prefab not found for: {screenName}");
                return null;
            }

            var screenInstance = _objectResolver.Instantiate(screenPrefab, _screenParent);
            _spawnedScreens.Add(screenName, screenInstance);

            InitializeScreen(screenInstance);

            return screenInstance;
        }

        private void InitializeScreen(RectTransform screen)
        {
            var view = screen.GetComponent<IScreenView>();
            if (view != null)
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

        private async UniTask MoveScreen(RectTransform screen, TransitionDirection direction)
        {
            var screenPosition = direction switch
            {
                TransitionDirection.Forward => _guiScreensConfig.Forward,
                TransitionDirection.Backward => _guiScreensConfig.Backward,
                TransitionDirection.Center => Vector2.zero,
                _ => _guiScreensConfig.Forward
            };

            await screen.DOAnchorPos(screenPosition, _guiScreensConfig.TransitionDuration)
                .SetEase(_guiScreensConfig.TransitionEase)
                .AsyncWaitForCompletion();
        }

        public void CleanUp()
        {
            _disposableMessage?.Dispose();
        }
    }
}
