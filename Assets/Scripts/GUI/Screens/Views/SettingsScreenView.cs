using UnityEngine.UI;

namespace GUI.Screens.Views
{
    public class SettingsScreenView : ScreenView
     {
        public Button MainMenuButton;
        public Button OneButtonPopup;
        public Button TwoButtonPopup;
        public override void Initialize()
        { 
            ID = GuiScreenIds.SettingsScreen;
        }
     }
}
