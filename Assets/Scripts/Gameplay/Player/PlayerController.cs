using System;
using Configs;
using Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay.Player
{
    public class PlayerController : IPlayerController
    {
        [Inject] private PlayerConfig _playerConfig;
        [Inject] private GameData _gameData;
        private readonly IObjectResolver _objectResolver;

        private PlayerView _playerView;
        private Camera _camera;

        public PlayerController(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public void Initialize(Transform parent)
        {
            var player = _objectResolver.Instantiate(_playerConfig
                .GetActiveShipPrefab(_gameData.ActiveShip.Id), parent);
            
            _playerView = player.GetComponent<PlayerView>();
            //TODO: Maybe create CameraManager cause it will needed for more advanced stuff
            _camera = Camera.main;
            
            SetActive(false);
        }

        public void Move(MovementDirection direction)
        {
            var velocity = direction switch
            {
                MovementDirection.Left => new Vector2(-_playerConfig.speed, 0),
                MovementDirection.Right => new Vector2(_playerConfig.speed, 0),
                MovementDirection.Up => new Vector2(0, _playerConfig.speed),
                MovementDirection.Down => new Vector2(0, -_playerConfig.speed),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

            _playerView.Rigidbody.linearVelocity = velocity;
        }

        public void Move(Vector2 direction)
        {
            var velocity = ApplyBoundaryDampening(direction * _playerConfig.speed);
            _playerView.Rigidbody.linearVelocity = velocity;
        }

        public void SetActive(bool active)
        {
            _playerView.gameObject.SetActive(active);
            if (active)
            {
                _playerView.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                _playerView.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
                _playerView.Rigidbody.linearVelocity = Vector2.zero;
                _playerView.transform.localPosition = _playerConfig.startPosition;
            }
        }
        
        
        //TODO: refactor this to store values at the Initialization rather on each update method
        Vector2 ApplyBoundaryDampening(Vector2 velocity)
        {
            var pos = _playerView.transform.position;
    
            // Get camera bounds
            var camHeight = _camera.orthographicSize;
            var camWidth = camHeight * _camera.aspect;
            var camPos = _camera.transform.position;

            var playerScaleX = _playerView.transform.localScale.x / 2;

            var minX = camPos.x - camWidth + playerScaleX;
            var maxX = camPos.x + camWidth - playerScaleX;
            //var minY = camPos.y - camHeight;
            //var maxY = camPos.y + camHeight;
    
            // Dampening distance from edge
            var dampeningZone = 0.1f;
    
            // Dampen X velocity near boundaries
            if (pos.x <= minX + dampeningZone && velocity.x < 0)
            {
                var factor = (pos.x - minX) / dampeningZone;
                velocity.x *= Mathf.Clamp01(factor);
            }
            else if (pos.x >= maxX - dampeningZone && velocity.x > 0)
            {
                var factor = (maxX - pos.x) / dampeningZone;
                velocity.x *= Mathf.Clamp01(factor);
            }
            /* Vertical boundary dampening not needed now
                    // Dampen Y velocity near boundaries
                    if (pos.y <= minY + dampeningZone && velocity.y < 0)
                    {
                        var factor = (pos.y - minY) / dampeningZone;
                        velocity.y *= Mathf.Clamp01(factor);
                    }
                    else if (pos.y >= maxY - dampeningZone && velocity.y > 0)
                    {
                        var factor = (maxY - pos.y) / dampeningZone;
                        velocity.y *= Mathf.Clamp01(factor);
                    }
            */
            return velocity;
        }
    }
}
