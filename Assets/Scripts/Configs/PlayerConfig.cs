using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private List<Ship> shipPrefabs;
        public Vector3 startPosition;
        public float speed = 5f;
        public float offsetPositionY = 0.5f;

        public GameObject GetActiveShipPrefab(string shipId)
        {
            var ship = shipPrefabs.Find(x => x.Id == shipId);
            if (ship == null)
            {
                Debug.LogError($"No ship found with id '{shipId}'");
                return null;
            }

            return ship.Prefab;
        }
    }

    [Serializable]
    public class Ship
    {
        public string Id;
        public GameObject Prefab;
    }
}