using Input;
using VContainer;

namespace Registrators
{
    public class InputRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.Register<IInputManager, InputManager>(Lifetime.Singleton);
        }
    }
}