using Cysharp.Threading.Tasks;

namespace Data.Load
{
    public interface ILoadManager
    {
        void Initialize();
        void Load();
    }
}