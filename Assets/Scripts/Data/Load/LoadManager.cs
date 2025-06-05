using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Data.Load
{
    public class LoadManager : ILoadManager
    {
        public async UniTask Initialize()
        {
            Debug.Log("LoadManager Initialized!");
            await UniTask.CompletedTask;
        }

        public async UniTask Load()
        {
            Debug.Log("Load data!");
            await UniTask.CompletedTask;
        }
    }
}