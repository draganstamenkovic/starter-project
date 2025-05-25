using UnityEngine;
using UnityEngine.UI;

namespace GUI.Screens.Views
{
    public class PlayScreenView : ScreenView
    { 
        public Button MainMenuButton;
        public override void Initialize()
        {
            ID = GuiScreenIds.PlayScreen;
        }
    }
}
