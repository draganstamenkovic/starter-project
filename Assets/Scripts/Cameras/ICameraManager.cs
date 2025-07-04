namespace Cameras
{
    public interface ICameraManager
    {
        void Initialize();
        UnityEngine.Camera GetMainCamera();
    }
}