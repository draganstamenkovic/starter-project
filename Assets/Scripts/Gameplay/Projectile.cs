using System;
using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Vector3 _direction;
        private Action<Projectile> _onCollisionHit;

        public void Initialize(Vector3 startingPosition,
                                Vector3 direction, 
                                Action<Projectile> onCollisionHit)
        {
            _onCollisionHit = onCollisionHit;
            _direction = direction.normalized;
            transform.position = startingPosition;
        }
        
        private void Update()
        {
            transform.position += _direction * (_speed * Time.deltaTime);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var enemyLayer =  LayerMask.NameToLayer(LayerIds.Enemy);
            var borderLayer =  LayerMask.NameToLayer(LayerIds.Border);
            
            if ((collision.gameObject.layer == enemyLayer ||
                collision.gameObject.layer == borderLayer) && gameObject.activeSelf)
            {
                _onCollisionHit?.Invoke(this);
            }
        }
    }
}