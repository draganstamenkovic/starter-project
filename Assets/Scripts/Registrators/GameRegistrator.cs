using Managers;
using VContainer;

namespace Registrators
{
    public class GameRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
        }
    }
}