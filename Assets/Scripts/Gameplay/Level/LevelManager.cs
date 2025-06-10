using Cysharp.Threading.Tasks;
using Data;
using VContainer;

namespace Gameplay.Level
{
    public class LevelManager : ILevelManager
    {
        [Inject] private readonly GameData _gameData;
        public UniTask Initialize()
        {

            return UniTask.CompletedTask;
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
            _gameData.CurrentLevel = _gameData.AllLevels[currentLevelIndex+1];
            
        }
    }
}