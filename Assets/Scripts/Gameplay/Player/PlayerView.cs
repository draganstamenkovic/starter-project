using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public Rigidbody2D Rigidbody => _rigidbody;
    }
}