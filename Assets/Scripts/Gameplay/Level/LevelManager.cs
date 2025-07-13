using Data;
using VContainer;

namespace Gameplay.Level
{
    public class LevelManager : ILevelManager
    {
        [Inject] private readonly GameData _gameData;
        public void Initialize()
        {
        }

        public void LoadLevel(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;
            _gameData.CurrentLevel = _gameData.AllLevels.Find(x => x.Id == id);
        }

        public void LoadNextLevel()
        {
            var currentLevelIndex = _gameData.AllLevels.FindIndex(level => level.Id == _gameData.CurrentLevel.Id);
            
            _gameData.CurrentLevel = GetLevelData(currentLevelIndex+1);
        }

        private LevelData GetLevelData(int levelIndex)
        {
            if (levelIndex < _gameData.AllLevels.Count)
                return _gameData.AllLevels[levelIndex];
            return _gameData.AllLevels[0];
        }
    }
}