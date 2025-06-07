using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay.Level
{
    public class LevelManager : ILevelManager
    {
        public List<LevelData> Levels { get; }
        public UniTask Initialize()
        {
            throw new System.NotImplementedException();
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