using Configs;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputConfig _inputConfig;
        private IPlayerController _playerController;
        
        private RectTransform _joystickTransform;
        private RectTransform _fireButtonTransform;
        private Button _fireButton;
        private VariableJoystick _joystick;
        private bool _isActive;
        
        public void Initialize(IPlayerController playerController)
        {
            _playerController = playerController;
            _joystickTransform = Instantiate(_inputConfig.joystickPrefab, transform);
            _fireButtonTransform = Instantiate(_inputConfig.fireButtonPrefab, transform);
            
            _joystick = _joystickTransform.GetComponent<VariableJoystick>();
            _fireButton = _fireButtonTransform.GetComponent<Button>();
            _joystickTransform.gameObject.SetActive(false);
            _fireButtonTransform.gameObject.SetActive(false);
        }

        private void OnFireButtonClicked()
        {
            _playerController.Fire();
        }

        public void SetActive(bool value)
        {
            _joystickTransform.gameObject.SetActive(value);
            _fireButtonTransform.gameObject.SetActive(value);
            _isActive = value;
            if (value)
            {
                _fireButton.onClick.AddListener(OnFireButtonClicked);
            }
            else
            {
                _fireButton.onClick.RemoveListener(OnFireButtonClicked);
            }
        }

        private void Update()
        {
            if (_isActive)
            {
                _playerController.Move(_joystick.Direction);
            }
        }
    }
}