using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Settings.Graphic
{
    public class FullScreenSettings : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        public Toggle Toggle => toggle;

        private void Start()
        {
            LoadFMode();
        }

        private void LoadFMode()
        {
            if (!PlayerPrefs.HasKey("FullScreenMode"))
            {
                SwitchFullScreen(true);
                return;
            }
            
            SwitchFullScreen(ReadFMode());
            toggle.isOn = ReadFMode();
        }
        
        public void SwitchFullScreen(bool value)
        {
            Screen.fullScreen = value;
            SaveFMode(value);
        }

        private void SaveFMode(bool value)
        {
            PlayerPrefs.SetInt("FullScreenMode", Convert.ToInt32(value));
        }

        private bool ReadFMode()
        {
            return Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenMode"));
        }
    }
}