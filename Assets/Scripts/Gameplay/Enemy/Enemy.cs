using Configs;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private string _id;
        public int _health;
        
        public void Initialize(EnemyData data)
        {
            _id = data.Id;
            _health = data.Health;
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
            Destroy(gameObject);
        }
    }
}
