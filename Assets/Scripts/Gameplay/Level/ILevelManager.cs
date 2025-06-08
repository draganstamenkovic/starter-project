using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay.Level
{
    public interface ILevelManager
    {
        List<LevelData> Levels { get; }
        UniTask Initialize(List<string> unlockedLevelsIds);
        string GetCurrentLevelId();
        LevelData GetCurrentLevel();
    }
}