using Cysharp.Threading.Tasks;

namespace Input
{
    public interface IInputManager
    {
        void Initialize();
        void SetActive(bool value);
    }
}