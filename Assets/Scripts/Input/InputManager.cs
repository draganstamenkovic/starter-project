using Configs;
using Gameplay.Player;
using UnityEngine;
using VContainer;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputConfig _inputConfig;
        private IPlayerController _playerController;
        
        private RectTransform _joystickTransform;
        private VariableJoystick _joystick;
        private bool _isActive;
        
        public void Initialize(IPlayerController playerController)
        {
            _playerController = playerController;
            _joystickTransform = Instantiate(_inputConfig.joystickPrefab, transform);
            _joystick = _joystickTransform.GetComponent<VariableJoystick>();
            SetActive(false);
        }

        public void SetActive(bool value)
        {
            _joystickTransform.gameObject.SetActive(value);
            _isActive = value;
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