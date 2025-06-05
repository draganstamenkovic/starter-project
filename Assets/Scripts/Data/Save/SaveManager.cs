using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Data.Save
{
    public class SaveManager : ISaveManager
    {
        public async UniTask Initialize()
        {
            Debug.Log("SaveManager initialized!");
            await UniTask.CompletedTask;
        }

        public async UniTask Save()
        {
            Debug.Log("Data saved");
            await UniTask.CompletedTask;
        }
    }
}