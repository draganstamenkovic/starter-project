using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GUI.Screens
{
    public interface IScreenManager
    {
        UniTask Initialize(Transform parent, GameObject screenBlocker);
        UniTask ShowScreen(string screenName, TransitionDirection direction = TransitionDirection.Center);
        UniTask HideScreen(string screenName, TransitionDirection direction = TransitionDirection.Forward);
    }
}