using Cysharp.Threading.Tasks;

namespace Audio.Managers
{
    public interface IAudioManager
    {
        UniTask Initialize();
        void PlaySfx(string sfxId);
        void PlayBackgroundMusic();
        void StopBackgroundMusic();
        void PauseBackgroundMusic();
    }
}