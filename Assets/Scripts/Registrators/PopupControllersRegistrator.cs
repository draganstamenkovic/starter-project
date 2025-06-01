using GUI.Popups.Controllers;
using VContainer;

namespace Registrators
{
    public class PopupControllersRegistrator
    {
        public static void Register(IContainerBuilder builder)
        {
            builder.Register<ConfirmationPopupController>(Lifetime.Singleton).As<IPopupController>();
            builder.Register<ShopPopupController>(Lifetime.Singleton).As<IPopupController>();
        }
    }
}