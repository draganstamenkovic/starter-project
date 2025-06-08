using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay.Level
{
    public class LevelManager : ILevelManager
    {
        public List<LevelData> Levels { get; }
        private Dictionary<string, LevelData> _unlockedLevels = new();
        public UniTask Initialize(List<string> unlockedLevelsIds)
        {
            foreach (var unlockedLevelId in unlockedLevelsIds)
            {
                
            }

            return UniTask.CompletedTask;
        }

        public string GetCurrentLevelId()
        {
            throw new System.NotImplementedException();
        }

        public LevelData GetCurrentLevel()
        {
            throw new System.NotImplementedException();
        }
    }
}