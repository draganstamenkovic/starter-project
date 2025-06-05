using Cysharp.Threading.Tasks;
using Player;
using UnityEngine;

public class PlayerController : IPlayerController
{
    private PlayerView _playerView;

    public async UniTask Initialize()
    { 
        await UniTask.CompletedTask;
    }

    public void SetPlayerView(PlayerView playerView)
    {
        _playerView = playerView;
    }
}
