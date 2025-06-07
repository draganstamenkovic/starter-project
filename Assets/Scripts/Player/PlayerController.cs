using System;
using Configs;
using Cysharp.Threading.Tasks;
using Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerController : IPlayerController
{
    [Inject] private PlayerConfig _playerConfig;
    private readonly IObjectResolver _objectResolver;
    
    private PlayerView _playerView;

    public PlayerController(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
    }

    public async UniTask Initialize()
    { 
        await UniTask.CompletedTask;
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

    private void ResetPosition()
    {
        
    }

    private void CreatePlayer()
    {
        var player = _objectResolver.Instantiate(_playerConfig.PlayerPrefab);
        var playerView = player.GetComponent<PlayerView>();
        _playerView = playerView;
    }

    public void SetActive(bool active)
    {
        _playerView.gameObject.SetActive(active);
    }
}
