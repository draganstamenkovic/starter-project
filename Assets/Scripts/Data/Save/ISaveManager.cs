using Cysharp.Threading.Tasks;

namespace Data.Save
{
    public interface ISaveManager
    {
        UniTask Save();
    }
}