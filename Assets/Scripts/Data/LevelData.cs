using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string Id;
        public int NumberOfEnemies;
        public bool Unlocked;
    }
}