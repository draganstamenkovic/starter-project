using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public float speed = 5f;
    }
}