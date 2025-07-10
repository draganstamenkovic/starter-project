using Cameras;
using Configs;
using Data;
using UnityEngine;
using VContainer;

namespace Gameplay.Enemy
{
    public class EnemiesManager : IEnemiesManager
    {
        [Inject] private readonly ICameraManager _cameraManager;
        [Inject] private readonly EnemiesConfig _enemiesConfig;
        [Inject] private readonly GameData _gameData;
        
        private Transform _gameplayParent;
        private Transform _enemyGridTransform;
        
        public void Initialize(Transform gameplayParent)
        {
            _gameplayParent = gameplayParent;
            SpawnEnemies(CreateGrid());
        }

        private EnemyGrid CreateGrid()
        {
            var enemyGrid = new EnemyGrid(_cameraManager, _enemiesConfig);

            _enemyGridTransform = new GameObject("EnemyGrid").transform;
            _enemyGridTransform.SetParent(_gameplayParent);

            return enemyGrid;
        }

        private void SpawnEnemies(EnemyGrid enemyGrid)
        {
            var defaultEnemy = _enemiesConfig.GetEnemy(EnemyIds.Default);
            
            for (var enemyIndex = 0; enemyIndex < _gameData.CurrentLevel.NumberOfEnemies; enemyIndex++)
            {
                var gridPos = enemyGrid.GetFreePosition();

                if (gridPos.x != -1 && gridPos.y != -1)
                {
                    if (enemyGrid.TryOccupyCell(gridPos.x, gridPos.y, out Vector3 spawnPosition))
                    {
                        Debug.Log("Index: " + enemyIndex);
                        var enemy = Object.Instantiate(defaultEnemy.Prefab, spawnPosition, Quaternion.identity,_enemyGridTransform);
                        
                        var enemyController = enemy.GetComponent<Enemy>();
                        enemyController.Initialize(defaultEnemy);
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