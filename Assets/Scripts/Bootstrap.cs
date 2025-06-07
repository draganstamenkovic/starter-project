using Audio.Managers;
using Cysharp.Threading.Tasks;
using Gameplay;
using GUI;
using Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class Bootstrap : IStartable
{
    [Inject] private GuiManager _guiManager;
    [Inject] private IGameManager _gameManager;
    [Inject] private IAudioManager _audioManager;
    [Inject] private IInputManager _inputManager;
    public void Start()
    {
        Debug.Log("Bootstrap started");
        InitializeGame().Forget();
    }

    private async UniTask InitializeGame()
    {
        Prepare();
        await _guiManager.Initialize().ContinueWith(() => 
              _audioManager.Initialize().ContinueWith(() => 
              _inputManager.Initialize().ContinueWith(() =>
              _gameManager.Initialize())));
    }

    private void Prepare()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
        #else
        Debug.logger.logEnabled = false;
#endif
    }
}
