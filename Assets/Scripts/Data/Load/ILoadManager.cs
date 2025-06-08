using Cysharp.Threading.Tasks;

namespace Data.Load
{
    public interface ILoadManager
    {
        UniTask Initialize();
        void Load();
    }
}