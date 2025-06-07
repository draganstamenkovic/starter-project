using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public int Id;
        public int NumberOfEnemies;
    }
}