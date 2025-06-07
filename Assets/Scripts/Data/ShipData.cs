using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "Game/Data/ShipData")]
    public class ShipData : ScriptableObject
    {
        public string Id;
        public Sprite Sprite;
    }
}