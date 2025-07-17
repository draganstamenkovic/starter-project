using Cameras;
using Configs;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyGrid
    {
        private readonly ICameraManager _cameraManager;
        private readonly EnemiesConfig _enemiesConfig;
        private float _cellSize = 1f;
        private int _width = 10;
        private int _height = 10;
        private Vector3 _gridOrigin;
        
        private bool[,] _gridOccupied;
        
        public EnemyGrid(ICameraManager cameraManager, EnemiesConfig enemiesConfig)
        {
            _cameraManager = cameraManager;
            _enemiesConfig = enemiesConfig;
            _gridOccupied = new bool[_width, _height];
            CreateGrid();
        }

        private void CreateGrid()
        {
            var orthoSize = _cameraManager.GetOrthographicSize();
            var aspect = _cameraManager.GetCameraAspect();

            var totalHeight = orthoSize * 2f;
            var totalWidth = totalHeight * aspect;

            var usableWidth = totalWidth - _enemiesConfig.GridPositionOffset - _enemiesConfig.GridPositionOffset;
            var usableHeight = (totalHeight / 2.5f) - _enemiesConfig.GridPositionOffset;

            _cellSize = _enemiesConfig.GridCellSize;

            _width = Mathf.FloorToInt(usableWidth / _cellSize);
            _height = Mathf.FloorToInt(usableHeight / _cellSize);

            var gridWorldWidth = _width * _cellSize;
            var gridWorldHeight = _height * _cellSize;

            var cameraCenter = _cameraManager.GetCameraPosition();
            var gridOriginX = (cameraCenter.x - (gridWorldWidth / 2f)) + _enemiesConfig.GridPositionOffset;
            var gridOriginY = cameraCenter.y + (gridWorldHeight / 2f) - _enemiesConfig.GridPositionOffset;

            _gridOrigin = new Vector3(gridOriginX, gridOriginY, 0f);
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return _gridOrigin + new Vector3(x * _cellSize, y * _cellSize, 0f);
        }

        public Vector2Int GetFreePosition()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (!_gridOccupied[x, y])
                        return new Vector2Int(x, y);
                }
            }
            return new Vector2Int(-1, -1); // No space left
        }

        public bool TryOccupyCell(int x, int y, out Vector3 worldPos)
        {
            worldPos = GetWorldPosition(x, y);
            if (x >= 0 && x < _width && y >= 0 && y < _height && !_gridOccupied[x, y])
            {
                _gridOccupied[x, y] = true;
                return true;
            }
            return false;
        }

        public void ClearOccupiedCells()
        {
            for (int x = 0; x < _width; x++)
            {
                for(int y = 0; y < _height; y++)
                    _gridOccupied[x, y] = false;
            }
        }
    }
}