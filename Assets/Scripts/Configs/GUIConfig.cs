using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GUIConfig", menuName = "Configs/GUIConfig")]
    public class GUIConfig : ScriptableObject
    {
        public float loadingRotateSpeed = 1f;
    }
}