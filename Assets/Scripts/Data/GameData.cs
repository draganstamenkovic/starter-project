using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class GameData
    {
        public List<string> UnlockedLevelsIds;
        public List<string> UnlockedShipsIds;
        public string SelectedShipId;
        public string CurrentLevelId;
        public int PlayerHighScore;
    }
}