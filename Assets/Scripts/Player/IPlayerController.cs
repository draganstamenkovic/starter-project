using Cysharp.Threading.Tasks;

namespace Player
{
    public interface IPlayerController
    {
        UniTask Initialize();
        void Move(MovementDirection direction);
        void SetActive(bool active);
    }
}