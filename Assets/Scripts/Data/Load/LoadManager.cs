using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Data.Load
{
    public class LoadManager : ILoadManager
    {
        [Inject] private GameData _gameData;
        private SerializableGameData _tempGameData;
        public void Initialize()
        {
            Debug.Log("LoadManager Initialized!");
            Load();
            FillData();
        }

        public void Load()
        {
            Debug.Log("Load data!");

            if (!File.Exists(DataManager.DataPath)) 
                _tempGameData = new SerializableGameData();
            else
            {
                var json = File.ReadAllText(DataManager.DataPath);
                _tempGameData = JsonUtility.FromJson<SerializableGameData>(json);
            }
        }

        private void FillData()
        {
            foreach (var level in _gameData.AllLevels)
            {
                Debug.Log(level.Id);
            }

            //TODO: Refactor this
            _gameData.PlayerHighScore = _tempGameData.PlayerHighScore;
            
            var activeShip = _gameData.AllShips.Find(ship => ship.Id.Equals(_tempGameData.ActiveShipId));
            _gameData.ActiveShip = activeShip;
            
            var currentLevel = _gameData.AllLevels.Find(level => level.Id.Equals(_tempGameData.CurrentLevelId));
            _gameData.CurrentLevel = currentLevel;
            
            foreach (var unlockedLevelId in _tempGameData.UnlockedLevelsIds)
            {
                _gameData.AllLevels.Find(x => 
                    x.Id.Equals(unlockedLevelId)).Unlocked = true;
            }

            foreach (var unlockedShipId in _tempGameData.UnlockedShipsIds)
            {
                _gameData.AllLevels.Find(x => 
                    x.Id.Equals(unlockedShipId)).Unlocked = true;
            }
        }
    }
}