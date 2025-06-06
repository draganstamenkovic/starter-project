using Cysharp.Threading.Tasks;

namespace Input
{
    public class InputManager : IInputManager
    {
        public async UniTask Initialize()
        {
            await UniTask.CompletedTask;
        }
    }
}