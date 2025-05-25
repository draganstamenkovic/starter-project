using GUI.Managers;
using VContainer;
using VContainer.Unity;

namespace Registrators
{
    public class GuiRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GuiManager>();
            builder.Register<IGuiScreenManager, GuiScreenManager>(Lifetime.Singleton);
        }
    }
}
