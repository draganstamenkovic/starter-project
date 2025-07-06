using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay.Level
{
    public interface ILevelManager
    {
        void Initialize();
        void LoadLevel(string id);
        void LoadNextLevel();
        
    }
}