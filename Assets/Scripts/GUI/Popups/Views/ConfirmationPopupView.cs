using System.Collections.Generic;
using GUI.Popups.Builder;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Popups.Views
{
    public class ConfirmationPopupView : MonoBehaviour, IPopupView
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _backgroundButton;
        [SerializeField] List<PopupButtonData> _buttons;
        public void Initialize(PopupData popupData)
        {
            _title.text = popupData.Title;
            _message.text = popupData.Text;
            if(popupData.Icon != null)
                _icon.sprite = popupData.Icon;
            SetupButtons(popupData.Buttons);
        }

        private void SetupButtons(List<PopupButton> buttonsData)
        {
            foreach (var buttonData in _buttons)
            {
                buttonData.Button.onClick.RemoveAllListeners();
                buttonData.Button.gameObject.SetActive(false);
            }
            
            _backgroundButton.onClick.RemoveListener(ClosePopup);
            
            _backgroundButton.onClick.AddListener(ClosePopup);
            for (int index = 0; index < buttonsData.Count; index++)
            {
                if (index < buttonsData.Count)
                {
                    _buttons[index].Text.text = buttonsData[index].Text;
                    _buttons[index].Color = buttonsData[index].Color;
                    var i = index;
                    _buttons[index].Button.onClick.AddListener(() =>
                    {
                        buttonsData[i].OnClick();
                    });
                    
                    _buttons[index].Button.gameObject.SetActive(true);
                }
                else
                {
                    _buttons[index].Button.gameObject.SetActive(false);
                }
            }
        }

        private void ClosePopup()
        {
            gameObject.SetActive(false);
        }
    }
}