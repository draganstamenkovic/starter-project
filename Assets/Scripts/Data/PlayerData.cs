using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public int HighScore;
        [FormerlySerializedAs("CurrentShip")] public ShipData currentShipData;
        public List<ShipData> UnlockedShips;
    }
}