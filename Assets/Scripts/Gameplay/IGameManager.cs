using System;
using Cysharp.Threading.Tasks;

namespace Gameplay
{
    public interface IGameManager
    {
        UniTask Initialize();
        void Play();
        void Pause();
        void Resume();
        void Quit();
        Action OnPause { get; set; }
        Action OnResume { get; set; }
        Action OnPlay { get; set; }
        Action OnStop { get; set; }
    }
}