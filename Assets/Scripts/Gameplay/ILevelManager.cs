using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay
{
    public interface ILevelManager
    {
        List<Level> Levels { get; }
        UniTask Initialize();
        string GetCurrentLevelId();
        Level GetCurrentLevel();
    }
}