using System.Collections.Generic;
using Configs;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class ProjectilePool : IProjectilePool
    {
        [Inject] private ProjectileConfig _projectileConfig;
        private IObjectResolver _objectResolver;
        private Transform _poolParent;
        
        private IObjectPool<Projectile> _projectilePool;

        public IObjectPool<Projectile> Pool
        {
            get
            {
                if (_projectilePool == null)
                {
                    _projectilePool = new ObjectPool<Projectile>
                    (
                        CreatePooledObject,
                        TakeFromPool,
                        ReturnToPool,
                        DestroyPooledObject,
                        _projectileConfig.collectionChecks,
                        _projectileConfig.defaultCapacity,
                        _projectileConfig.maxCapacity
                    );
                }

                return _projectilePool;
            }
        }

        public void Initialize(IObjectResolver objectResolver, Transform gameplayParent)
        {
            _objectResolver = objectResolver;
            
            var projectilePool = new GameObject(nameof(ProjectilePool));
            projectilePool.transform.SetParent(gameplayParent);
            
            _poolParent = projectilePool.transform;
            
            Prewarm();
        }

        private void Prewarm()
        {
            var tempProjectileList = new List<Projectile>();

            for (int i = 0; i < _projectileConfig.defaultCapacity; i++)
            {
                var projectile = Pool.Get();
                tempProjectileList.Add(projectile);
            }

            foreach (var projectile in tempProjectileList)
            {
                Pool.Release(projectile);
            }
        }

        private Projectile CreatePooledObject()
        {
            var projectile = _objectResolver.Instantiate(
                _projectileConfig.GetProjectilePrefab("Default"),
                _poolParent, true);
            
            projectile.gameObject.SetActive(false);
            return projectile;
        }
        private void TakeFromPool(Projectile bullet)
        {
            bullet.gameObject.SetActive(true);
        }
        private void ReturnToPool(Projectile bullet)
        {
            bullet.gameObject.SetActive(false);
        }
        private void DestroyPooledObject(Projectile bullet)
        {
            Object.Destroy(bullet);
        }
    }
}