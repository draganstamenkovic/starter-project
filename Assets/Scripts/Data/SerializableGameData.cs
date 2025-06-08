using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class SerializableGameData
    {
        public List<string> UnlockedLevelsIds = new();
        public List<string> UnlockedShipsIds = new();
        public string ActiveShipId;
        public string CurrentLevelId;
        public int PlayerHighScore;
    }
}