using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Settings.Graphic
{
    public class FullScreenSettings : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        public Toggle Toggle => toggle;

        public void SwitchFullScreen(bool value)
        {
            Screen.fullScreen = value;
        }
    }
}