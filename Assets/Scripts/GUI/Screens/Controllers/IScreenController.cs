using GUI.Screens.Views;

namespace GUI.Screens.Controllers
{
    public interface IScreenController
    {
        string ID { get; }
        void SetView(IScreenView view);
        void Initialize(IScreenManager screenManager);
    }
}