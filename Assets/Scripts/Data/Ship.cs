using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Game/Data/Ship")]
    public class Ship : ScriptableObject
    {
        public string Id;
        public Sprite Sprite;
    }
}