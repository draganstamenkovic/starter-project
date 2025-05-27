using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Popups.Views
{
    [Serializable]
    public class PopupButtonData
    {
        public TextMeshProUGUI Text;
        public Color Color;
        public Button Button;
    }
}