using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public interface IEnemiesManager
    {
        void Initialize(Transform gameplayParent, Action onEnemiesDestroyed);
        void SetActive(bool isActive);
        Action OnEnemiesDestroyed {get; set;}
    }
}