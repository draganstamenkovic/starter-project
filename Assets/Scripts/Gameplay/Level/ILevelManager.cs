using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay.Level
{
    public interface ILevelManager
    {
        List<LevelData> Levels { get; }
        UniTask Initialize();
        string GetCurrentLevelId();
        LevelData GetCurrentLevel();
    }
}