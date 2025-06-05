using Cysharp.Threading.Tasks;

namespace Player
{
    public interface IPlayerController
    {
        UniTask Initialize();
        void SetPlayerView(PlayerView playerView);
    }
}