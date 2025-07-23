using System;
using Cameras;
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
        [Inject] private IProjectilePool _projectilePool;
        [Inject] private ICameraManager _cameraManager;
        private readonly IObjectResolver _objectResolver;

        private PlayerView _playerView;

        private float _camHeight;
        private float _camWidth;
        private float _playerScaleX;
        private float _minX;
        private float _maxX;
        private float _dampeningZone = 0.1f;
        private Vector3 _camPosition;
        private Vector3 _playerPosition;
        private Vector3 _startPosition;
        public PlayerController(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public void Initialize(Transform gameplayParent)
        {
            var player = _objectResolver.Instantiate(_playerConfig
                .GetActiveShipPrefab(_gameData.ActiveShip.Id), gameplayParent);
            player.SetActive(false);
            var positionY = -_cameraManager.GetOrthographicSize()
                            + player.transform.localScale.y 
                            + _playerConfig.offsetPositionY;
            _startPosition = new Vector3(0,positionY,0);
            player.transform.position = _startPosition;

            _playerView = player.GetComponent<PlayerView>();
            _projectilePool.Initialize(_objectResolver, gameplayParent);
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

        public void Fire()
        {
            SpawnProjectile();
        }

        public void SetActive(bool active)
        {
            _playerView.gameObject.SetActive(active);
            _projectilePool.SetActive(active);
            if (active)
            {
                _playerView.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                _playerView.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
                _playerView.Rigidbody.linearVelocity = Vector2.zero;
                _playerView.transform.localPosition = _startPosition;
            }
        }

        private void SpawnProjectile()
        {
            var projectile = _projectilePool.Pool.Get();
            
            projectile.Initialize(_playerView.CannonTransform.position,
                         _playerView.CannonTransform.up,
                                    OnProjectileCollision);
        }

        private void OnProjectileCollision(Projectile projectile)
        {
            _projectilePool.Pool.Release(projectile);
        }

        private Vector2 ApplyBoundaryDampening(Vector2 velocity)
        {
            _playerPosition = _playerView.transform.position;
    
            // Get camera bounds
            _camWidth = _cameraManager.GetCameraWidth();
            _camPosition = _cameraManager.GetCameraPosition();

            _playerScaleX = _playerView.transform.localScale.x / 2;

            var minX = _camPosition.x - _camWidth + _playerScaleX;
            var maxX = _camPosition.x + _camWidth - _playerScaleX;
    
            // Dampen X velocity near boundaries
            if (_playerPosition.x <= minX + _dampeningZone && velocity.x < 0)
            {
                var factor = (_playerPosition.x - minX) / _dampeningZone;
                velocity.x *= Mathf.Clamp01(factor);
            }
            else if (_playerPosition.x >= maxX - _dampeningZone && velocity.x > 0)
            {
                var factor = (maxX - _playerPosition.x) / _dampeningZone;
                velocity.x *= Mathf.Clamp01(factor);
            }

            return velocity;
        }
    }
}
