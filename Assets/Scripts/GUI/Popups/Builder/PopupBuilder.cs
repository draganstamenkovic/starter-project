using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUI.Popups.Builder
{
    public class PopupBuilder : IPopupBuilder
    {
        private PopupData _popupData = new ();

        public PopupBuilder()
        {
            Reset();
        }

        public IPopupBuilder Title(string title)
        {
            _popupData.Title = title;
            return this;
        }

        public IPopupBuilder Text(string text)
        {
            _popupData.Text = text;
            return this;
        }

        public IPopupBuilder AddButton(string buttonText, Color color, Action onButtonClick)
        {
            if (_popupData.Buttons == null)
                _popupData.Buttons = new List<PopupButton>();

            _popupData.Buttons.Add(new PopupButton
            {
                Text = buttonText,
                Color = color,
                OnClick = onButtonClick
            });
            return this;
        }

        private void Reset()
        {
            if(_popupData.Buttons != null)
                _popupData.Buttons.Clear();
            _popupData.Title = string.Empty;
            _popupData.Text = string.Empty;
            _popupData.Icon = null;
        }

        public PopupData Build()
        {
            return _popupData;
        }
    }
}