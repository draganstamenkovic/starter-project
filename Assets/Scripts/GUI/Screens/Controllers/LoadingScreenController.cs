using GUI.Managers;
using GUI.Screens.Views;
using UnityEngine;

namespace GUI.Screens.Controllers
{
    public class LoadingScreenController : IScreenController
    {
        private IGuiScreenManager _guiScreenManager;
        private LoadingScreenView _view;
        public string ID => GuiScreenIds.LoadingScreen;
        public void SetView(IScreenView view)
        {
            _view = view as LoadingScreenView;
        }

        public void Initialize(IGuiScreenManager guiScreenManager)
        {
            _guiScreenManager = guiScreenManager;
            Debug.Log("Initializing Loading Screen");
        }
    }
}