using System;
using UnityEngine;

namespace GUI.Screens.Views
{
    public interface IScreenView
    {
        string ID { get; set; }
        RectTransform RectTransform { get; }
        Action OnShow { get; set; }
        Action OnShown { get; set; }
        Action OnHide { get; set; }
        Action OnHidden { get; set; }
        void Initialize();
    }
}