using Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace GUI.Managers
{
    public class GuiManager : MonoBehaviour
    {
        [Inject] private IGuiScreenManager _screenManager;
        [Inject] private GUIConfig _guiConfig;

        [SerializeField] private Transform _guiScreensContainer;
        [SerializeField] private Transform _guiSpinner;
        [SerializeField] private GameObject _guiScreenBlocker;
        private bool _activeSpinner;
        public async UniTask Initialize()
        {
            Debug.Log("GUIManager Initialized");
            SetSpinnerActive(true);
            await UniTask.Delay(500);
            await _screenManager.Initialize(_guiScreensContainer, _guiScreenBlocker)
                .ContinueWith(() => SetSpinnerActive(false));
        }

        public void SetSpinnerActive(bool active)
        {
            _activeSpinner = active;
            _guiSpinner.gameObject.SetActive(active);
        }

        private void Update()
        {
            if(_activeSpinner)
                _guiSpinner.transform.Rotate(new Vector3(0, 0, _guiConfig.loadingRotateSpeed));
        }
        
        
    }
}
