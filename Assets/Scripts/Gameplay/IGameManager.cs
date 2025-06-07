using Cysharp.Threading.Tasks;

namespace Gameplay
{
    public interface IGameManager
    {
        UniTask ShowMainMenu();
        UniTask Initialize();
        void Play();
        void Pause();
        void Resume();
        void Quit();
    }
}