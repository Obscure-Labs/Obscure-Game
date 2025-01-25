using UnityEngine;

namespace DefaultNamespace
{
    public class BaseControl : MonoBehaviour
    {
        void Start()
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow, 60);
        }
    }
}