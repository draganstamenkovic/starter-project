using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "Game/Data/Level")]
    public class Level : ScriptableObject
    {
        public int Id;
        public int NumberOfEnemies;
    }
}