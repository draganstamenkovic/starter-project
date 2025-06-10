using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay.Level
{
    public interface ILevelManager
    {
        UniTask Initialize();
        void LoadLevel(string id);
        void LoadNextLevel();
        
    }
}