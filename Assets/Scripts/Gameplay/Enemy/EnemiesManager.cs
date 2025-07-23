using System;
using Audio;
using Cameras;
using Configs;
using Data;
using GUI.Popups;
using Message;
using Message.Messages;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace Gameplay.Enemy
{
    public class EnemiesManager : IEnemiesManager
    {
        [Inject] private readonly ICameraManager _cameraManager;
        [Inject] private readonly IMessageBroker _messageBroker;
        [Inject] private readonly EnemiesConfig _enemiesConfig;
        [Inject] private readonly GameData _gameData;
        
        private Transform _gameplayParent;
        private GameObject _enemyGridObject;
        private EnemyGrid _enemyGrid;

        public Action OnEnemiesDestroyed { get; set; }
        
        public void Initialize(Transform gameplayParent)
        {
            _gameplayParent = gameplayParent;
            _enemyGrid = CreateGrid();
        }

        public void SetActive(bool isActive)
        {
            _enemyGridObject.SetActive(isActive);

            if (isActive)
            {
                SpawnEnemies();
            }
            else
            {
                foreach (Transform enemy in _enemyGridObject.transform)
                {
                    Object.Destroy(enemy.gameObject);
                }
                _enemyGrid.ClearOccupiedCells();
            }
        }

        private EnemyGrid CreateGrid()
        {
            var enemyGrid = new EnemyGrid(_cameraManager, _enemiesConfig);

            _enemyGridObject = new GameObject("EnemyGrid");
            _enemyGridObject.SetActive(false);
            _enemyGridObject.transform.SetParent(_gameplayParent);

            return enemyGrid;
        }

        private void SpawnEnemies()
        {
            var defaultEnemy = _enemiesConfig.GetEnemy(EnemyIds.Default);
            var enemiesCount = 0;
            for (var enemyIndex = 0; enemyIndex < _gameData.CurrentLevel.NumberOfEnemies; enemyIndex++)
            {
                var gridPos = _enemyGrid.GetFreePosition();

                if (gridPos.x != -1 && gridPos.y != -1)
                {
                    if (_enemyGrid.TryOccupyCell(gridPos.x, gridPos.y, out Vector3 spawnPosition))
                    {
                        var enemy = Object.Instantiate(defaultEnemy.Prefab, spawnPosition, 
                            Quaternion.identity,
                            _enemyGridObject.transform);
                        enemiesCount++;
                        var enemyController = enemy.GetComponent<Enemy>();
                        enemyController.Initialize(defaultEnemy, () =>
                        {
                            _messageBroker.Publish(new PlaySfxMessage(AudioIds.EnemyShipDestroyed));
                            enemiesCount--;
                            if (enemiesCount > 0) return;
                                _messageBroker.Publish(new ShowPopupMessage(PopupIds.LevelFinishedPopup));
                                OnEnemiesDestroyed?.Invoke();
                        });
                    }
                    else
                    {
                        Debug.Log("Cell is already occupied!\nIndex: " + enemyIndex);
                    }
                }
                else
                {
                    Debug.Log("Grid is full!");
                }
            }
        }
    }
}