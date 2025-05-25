using Cysharp.Threading.Tasks;

namespace Audio.Managers
{
    public interface IAudioManager
    {
        UniTask Initialize();
        void Play();
        void Stop();
        void Pause();
    }
}