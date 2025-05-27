using Configs;
using Cysharp.Threading.Tasks;
using GUI.Popups;
using GUI.Popups.Builder;
using GUI.Screens;
using UnityEngine;
using VContainer;

namespace GUI
{
    public class GuiManager : MonoBehaviour
    {
        [Inject] private IScreenManager _screenManager;
        [Inject] private IPopupManager _popupManager;
        [Inject] private IPopupBuilder _popupBuilder;
        [Inject] private GUIConfig _guiConfig;

        [SerializeField] private Transform _screensContainer;
        [SerializeField] private Transform _popupsContainer;
        
        [SerializeField] private Transform _spinner;
        [SerializeField] private GameObject _screenBlocker;
        private bool _activeSpinner;
        public async UniTask Initialize()
        {
            Debug.Log("GUIManager Initialized");
            SetSpinnerActive(true);
            await _screenManager.Initialize(_screensContainer, _screenBlocker)
                    .ContinueWith(() => _popupManager.Initialize(_popupsContainer, _screenBlocker))
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

        [ContextMenu("Show popup")]
        public void ShowPopup()
        {
            var popupData = _popupBuilder.Title("Naslov")
                .Text("Ovo je neki tekst da vidimo kako izgleda")
                .AddButton("Dugme", Color.blue, () =>
                {
                    Debug.Log("GUIManager ShowPopup");
                })
                .Build();
            _popupManager.ShowConfirmationPopup(popupData);
        }
    }
}
