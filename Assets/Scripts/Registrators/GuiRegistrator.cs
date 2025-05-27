using GUI;
using GUI.Popups;
using GUI.Popups.Builder;
using GUI.Screens;
using VContainer;
using VContainer.Unity;

namespace Registrators
{
    public class GuiRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GuiManager>();
            builder.Register<IScreenManager, ScreenManager>(Lifetime.Singleton);
            builder.Register<IPopupBuilder, PopupBuilder>(Lifetime.Singleton);
            builder.Register<IPopupManager, PopupManager>(Lifetime.Singleton);
        }
    }
}
