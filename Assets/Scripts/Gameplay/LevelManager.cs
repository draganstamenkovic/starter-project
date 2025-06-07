using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay
{
    public class LevelManager : ILevelManager
    {
        public List<Level> Levels { get; }
        public UniTask Initialize()
        {
            throw new System.NotImplementedException();
        }

        public string GetCurrentLevelId()
        {
            throw new System.NotImplementedException();
        }

        public Level GetCurrentLevel()
        {
            throw new System.NotImplementedException();
        }
    }
}