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

        public PlayerController(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public void Initialize(Transform parent)
        {
            var player = _objectResolver.Instantiate(_playerConfig
                .GetActiveShipPrefab(_gameData.ActiveShip.Id), parent);
            
            _playerView = player.GetComponent<PlayerView>();
            
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
    }
}
