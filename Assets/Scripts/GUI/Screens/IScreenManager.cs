using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GUI.Screens
{
    public interface IScreenManager
    {
        void Initialize(Transform parent, GameObject screenBlocker);
        UniTask ShowScreen(string screenName);
        UniTask HideScreen(string screenName);
    }
}