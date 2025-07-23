using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public interface IEnemiesManager
    {
        Action OnEnemiesDestroyed { get; set; }
        void Initialize(Transform gameplayParent);
        void SetActive(bool isActive);
    }
}