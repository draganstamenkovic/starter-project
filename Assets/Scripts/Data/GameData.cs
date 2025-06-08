using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game/Data/GameData")]
    public class GameData : ScriptableObject
    {
        public List<LevelData> AllLevels;
        public List<ShipData> AllShips;
        public ShipData ActiveShip;
        public LevelData CurrentLevel;
        public int PlayerHighScore;
    }
}