using UnityEngine;
namespace Cameras
{
    public class CameraManager : ICameraManager
    {
        private Camera _mainCamera;
        public void Initialize()
        {
            _mainCamera = Camera.main;
        }

        public Camera GetMainCamera()
        {
            return _mainCamera;
        }
    }
}