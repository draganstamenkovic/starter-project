using Player;
using VContainer;

namespace Registrators
{
    public class PlayerRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.Register<IPlayerController, PlayerController>(Lifetime.Singleton);
        }
    }
}