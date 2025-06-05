using Data.Load;
using Data.Save;
using VContainer;

namespace Registrators
{
    public class DataRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.Register<ISaveManager, SaveManager>(Lifetime.Singleton);
            builder.Register<ILoadManager, LoadManager>(Lifetime.Singleton);
        }
    }
}