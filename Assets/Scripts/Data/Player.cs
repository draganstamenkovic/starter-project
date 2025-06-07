using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Player
    {
        public int HighScore;
        public Ship CurrentShip;
        public List<Ship> UnlockedShips;
    }
}