using Cameras;
using VContainer;

namespace Gameplay.Enemy
{
    public class EnemyManager : IEnemyManager
    {
        [Inject] private readonly ICameraManager _cameraManager;

        private float _cameraWidth;
        public void Initialize()
        {
            //TODO: get ortographic width and find bounds and create grid based system for enemies placements
            _cameraWidth = _cameraManager.GetMainCamera().orthographicSize / 2;
            CreateGrid();
            SpawnEnemies();
        }

        private void CreateGrid()
        {
            
        }

        private void SpawnEnemies()
        {
            
        }
    }
}