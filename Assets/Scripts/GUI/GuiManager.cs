using Configs;
using Cysharp.Threading.Tasks;
using GUI.Popups;
using GUI.Popups.Builder;
using GUI.Screens;
using Input;
using UnityEngine;
using VContainer;

namespace GUI
{
    public class GuiManager : MonoBehaviour
    {
        [Inject] private readonly IScreenManager _screenManager;
        [Inject] private readonly IPopupManager _popupManager;
        [Inject] private readonly IPopupBuilder _popupBuilder;
        [Inject] private GUIConfig _guiConfig;

        [SerializeField] private Transform _screensContainer;
        [SerializeField] private Transform _popupsContainer;
        
        [SerializeField] private Transform _spinner;
        [SerializeField] private GameObject _screenBlocker;
        private bool _activeSpinner;
        public async UniTask Initialize()
        {
            SetSpinnerActive(true);
            _screenManager.Initialize(_screensContainer, _screenBlocker);
            _popupManager.Initialize(_popupsContainer, _screenBlocker);
            
            await _screenManager.ShowScreen(GuiScreenIds.MainMenuScreen)
                .ContinueWith(() => SetSpinnerActive(false));
        }

        public void SetSpinnerActive(bool active)
        {
            _activeSpinner = active;
            _spinner.gameObject.SetActive(active);
        }

        private void Update()
        {
            if(_activeSpinner)
                _spinner.transform.Rotate(new Vector3(0, 0, _guiConfig.loadingRotateSpeed));
        }
    }
}
