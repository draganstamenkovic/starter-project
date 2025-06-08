using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Data.Save
{
    public class SaveManager : ISaveManager
    {
        [Inject] private readonly GameData _gameData;
        private SerializableGameData _tempGameData;
        public async UniTask Initialize()
        {
            PopulateData();
            await Save();
        }

        public async UniTask Save()
        {
            Debug.Log("Data saved");
            var savePath = DataManager.DataPath;
            string json = JsonUtility.ToJson(_tempGameData, prettyPrint: true);
            await File.WriteAllTextAsync(savePath, json);
        
#if UNITY_EDITOR
            Debug.Log($"Game saved to: {savePath}\n{json}");
#endif
        }

        private void PopulateData()
        {
            _tempGameData = new SerializableGameData
            {
                CurrentLevelId = _gameData.CurrentLevel.Id,
                ActiveShipId = _gameData.ActiveShip.Id,
                PlayerHighScore = _gameData.PlayerHighScore
            };

            var unlockedLevels = _gameData.AllLevels.FindAll(level => level.Unlocked);
            foreach (var levelData in unlockedLevels)
            {
                _tempGameData.UnlockedLevelsIds.Add(levelData.Id);
            }
            
            var unlockedShips = _gameData.AllShips.FindAll(ship => ship.Unlocked);
            foreach (var shipData in unlockedShips)
            {
                _tempGameData.UnlockedShipsIds.Add(shipData.Id);
            }
        }
    }
}