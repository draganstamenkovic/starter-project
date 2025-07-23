using Configs;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace Gameplay
{
    public interface IProjectilePool
    {
        IObjectPool<Projectile> Pool { get; }
        void Initialize(IObjectResolver objectResolver, Transform gameplayParent);
        void SetActive(bool active);
    }
}