using System.Threading;
using Audio.Managers;
using Cysharp.Threading.Tasks;
using Gameplay;
using GUI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class Bootstrap : IAsyncStartable
{
    [Inject] private GuiManager _guiManager;
    [Inject] private IGameManager _gameManager;
    [Inject] private IAudioManager _audioManager;

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        Prepare();
        await _audioManager.Initialize();
        await _gameManager.Initialize();
        await _guiManager.Initialize();
        _audioManager.PlayBackgroundMusic();
    }
    private void Prepare()
    {
        Application.targetFrameRate = 60;
    }
}
