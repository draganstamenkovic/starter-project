using Cysharp.Threading.Tasks;

namespace Managers
{
    public interface IGameManager
    {
        UniTask StartGame();
        UniTask Initialize();
    }
}