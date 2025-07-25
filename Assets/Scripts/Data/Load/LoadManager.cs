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
            Load();
            FillData();
        }

        public void Load()
        {
            if (!File.Exists(DataManager.DataPath))
            {
                _tempGameData = new SerializableGameData();
            }
            else
            {
                var json = File.ReadAllText(DataManager.DataPath);
                _tempGameData = JsonUtility.FromJson<SerializableGameData>(json);
            }
        }

        private void FillData()
        {
            //TODO: Refactor this
            _gameData.PlayerHighScore = _tempGameData.PlayerHighScore;
            
            var activeShip = _gameData.AllShips.Find(ship => ship.Id.Equals(_tempGameData.ActiveShipId));
            _gameData.ActiveShip = activeShip != null 
                ? activeShip : _gameData.AllShips[0];
            
            var currentLevel = _gameData.AllLevels.Find(level => level.Id.Equals(_tempGameData.CurrentLevelId));
            if(currentLevel != null)
                _gameData.CurrentLevel = currentLevel != null 
                    ? currentLevel : _gameData.AllLevels[0];
            
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