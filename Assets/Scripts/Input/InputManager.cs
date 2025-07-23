using Configs;
using Gameplay.Player;
using Helpers.RuntimeInfo;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        [Inject] private readonly IRuntimeInformation _runtimeInformation;
        [SerializeField] private InputConfig _inputConfig;
        private IPlayerController _playerController;
        
        private RectTransform _joystickTransform;
        private RectTransform _fireButtonTransform;
        private Button _fireButton;
        private VariableJoystick _joystick;
        private bool _isActive;

        private InputAction _moveAction;
        private InputAction _fireAction;
        
        public void Initialize(IPlayerController playerController)
        {
            _playerController = playerController;
            if(_runtimeInformation.OSPlatform == RuntimeOSPlatform.Editor)
                InitializePCInput();
            else
                InitializeMobileInput();
        }

        private void InitializeMobileInput()
        {
            _joystickTransform = Instantiate(_inputConfig.joystickPrefab, transform);
            _fireButtonTransform = Instantiate(_inputConfig.fireButtonPrefab, transform);
            
            _joystick = _joystickTransform.GetComponent<VariableJoystick>();
            _fireButton = _fireButtonTransform.GetComponent<Button>();
            _joystickTransform.gameObject.SetActive(false);
            _fireButtonTransform.gameObject.SetActive(false);
        }

        private void InitializePCInput()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _fireAction = InputSystem.actions.FindAction("Fire");
        }

        private void OnFireButtonClicked()
        {
            _playerController.Fire();
        }

        public void SetActive(bool value)
        {
            if (_runtimeInformation.OSPlatform == RuntimeOSPlatform.Editor)
            {
                if (value)
                { 
                    _moveAction.Enable();
                    _fireAction.Enable();

                    _fireAction.performed += context => _playerController.Fire();
                }
                else
                {
                    _moveAction.Disable();
                    _fireAction.Disable();
                    
                    _fireAction.performed -= context => _playerController.Fire();
                }
            }
            else
            {
                _joystickTransform.gameObject.SetActive(value);
                _fireButtonTransform.gameObject.SetActive(value);
                
                if (value)
                    _fireButton.onClick.AddListener(OnFireButtonClicked);
                else
                    _fireButton.onClick.RemoveListener(OnFireButtonClicked);
            }

            _isActive = value;
        }

        private void Update()
        {
            if (!_isActive) return;
            if (_runtimeInformation.OSPlatform == RuntimeOSPlatform.Editor)
            {
                var moveValue = _moveAction.ReadValue<Vector2>();
                _playerController.Move(moveValue);
            }
            else
                _playerController.Move(_joystick.Direction);
        }
    }
}