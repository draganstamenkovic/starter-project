using UnityEngine;
using UnityEngine.UI;

namespace GUI.Screens.Views
{
    public class MainMenuScreenView : ScreenView
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _playButton;

        public Button SettingsButton => _settingsButton;
        public Button PlayButton => _playButton;
        
        public override void Initialize()
        {
            ID = GuiScreenIds.MainMenuScreen;
            Debug.Log("Initialized View");
        }
    }
}