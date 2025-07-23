using System.Threading;
using Audio.Managers;
using Cysharp.Threading.Tasks;
using Gameplay;
using GUI;
using Helpers.RuntimeInfo;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class Bootstrap : IAsyncStartable
{
    [Inject] private GuiManager _guiManager;
    [Inject] private IGameManager _gameManager;
    [Inject] private IAudioManager _audioManager;
    [Inject] private IRuntimeInformation _runtimeInformation;

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        Prepare();
        _runtimeInformation.Initialize();
        _audioManager.Initialize();
        _gameManager.Initialize();
        await _guiManager.Initialize();
    }
    private void Prepare()
    {
        Application.targetFrameRate = 60;
    }
}
