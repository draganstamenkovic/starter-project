using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public interface IEnemiesManager
    {
        void Initialize(Transform gameplayParent);
        void SetActive(bool isActive);
    }
}