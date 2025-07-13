using System;
using Configs;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private Action _onDestroyed;
        
        private string _id;
        private int _health;
        
        public void Initialize(EnemyData data, Action onDestroyed)
        {
            _id = data.Id;
            _health = data.Health;
            _onDestroyed = onDestroyed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer(LayerIds.Projectile)) 
                return;
            
            _health--;
            
            if (_health != 0) 
                return;
            
            var particle = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            particle.Play();
            _onDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
