using System.Collections.Generic;
using System.Linq;
using Configs;
using SRF;
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
        private List<Projectile> _projectiles;

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
            projectilePool.SetActive(false);
            projectilePool.transform.SetParent(gameplayParent);
            
            _poolParent = projectilePool.transform;
            Prewarm();
        }

        private void Prewarm()
        {
            _projectiles = new List<Projectile>();

            for (int i = 0; i < _projectileConfig.defaultCapacity; i++)
            {
                var projectile = Pool.Get();
                _projectiles.Add(projectile);
            }

            foreach (var projectile in _projectiles)
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
        private void TakeFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }
        private void ReturnToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }
        private void DestroyPooledObject(Projectile projectile)
        {
            Object.Destroy(projectile);
        }

        public void SetActive(bool active)
        {
            if (!active)
            {
                foreach (var projectile in _projectiles.Where(projectile => projectile.isActiveAndEnabled))
                {
                    Pool.Release(projectile);
                }
            }
            _poolParent.gameObject.SetActive(active);
        }
    }
}