using Cysharp.Threading.Tasks;

namespace Managers
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