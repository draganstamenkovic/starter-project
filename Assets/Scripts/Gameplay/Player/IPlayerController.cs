using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerController
    {
        void Initialize(Transform parent);
        void Move(MovementDirection direction);
        void SetActive(bool active);
    }
}