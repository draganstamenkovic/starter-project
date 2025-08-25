using System.Collections.Generic;
using System.Linq;
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

        private Transform _poolParent;   // hidden storage for inactive projectiles
        private Transform _activeParent; // visible parent for active projectiles

        private IObjectPool<Projectile> _projectilePool;
        private readonly HashSet<Projectile> _activeProjectiles = new();


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

            var root = new GameObject(nameof(ProjectilePool));
            root.transform.SetParent(gameplayParent);

            _activeParent = new GameObject("ActiveProjectiles").transform;
            _activeParent.SetParent(root.transform);

            _poolParent = new GameObject("PooledProjectiles").transform;
            _poolParent.SetParent(root.transform);
            _poolParent.gameObject.SetActive(false);

            Prewarm();
        }

        public void ReleaseAll()
        {
            foreach (var projectile in _activeProjectiles.ToArray())
            {
                Pool.Release(projectile);
            }
            _activeProjectiles.Clear();
        }

        private void Prewarm()
        {
            for (int i = 0; i < _projectileConfig.defaultCapacity; i++)
            {
                var projectile = Pool.Get();
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
            projectile.transform.SetParent(_activeParent);
            projectile.gameObject.SetActive(true);
            _activeProjectiles.Add(projectile);
        }

        private void ReturnToPool(Projectile projectile)
        {
            projectile.transform.SetParent(_poolParent);
            projectile.gameObject.SetActive(false);
            _activeProjectiles.Remove(projectile);
        }

        private void DestroyPooledObject(Projectile projectile)
        {
            Object.Destroy(projectile.gameObject);
        }
    }
}
