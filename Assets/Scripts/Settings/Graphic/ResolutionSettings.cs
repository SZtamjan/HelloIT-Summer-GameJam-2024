using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings.Graphic
{
    public class ResolutionSettings : MonoBehaviour
    {
        private Resolution[] _resolutions;
        [SerializeField] private TMP_Dropdown resDropdown;

        private void Start()
        {
            SetUpDropdown();
        }

        private void SetUpDropdown()
        {
            _resolutions = Screen.resolutions;
            
            resDropdown.ClearOptions();

            List<string> resolutions = new List<string>();

            int currResIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                double a = _resolutions[i].refreshRateRatio.value;
                string resolution = _resolutions[i].width + " x " + _resolutions[i].height + " @" + Mathf.RoundToInt((float)a);
                resolutions.Add(resolution);
                
                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    currResIndex = i;
                }
            }
            
            resDropdown.AddOptions(resolutions);
            resDropdown.value = currResIndex;
            resDropdown.RefreshShownValue();
        }

        public void SetResolution(int resIndex)
        {
            Resolution res = _resolutions[resIndex];
            
            TryGetComponent(out FullScreenSettings fullScreenSettings);
            if (fullScreenSettings == null)
            {
                Debug.LogError("skrypt ResolutionSettings i FullScreenSettings musi byc na jednym obiekcie");
                return;
            }
            
            Screen.SetResolution(res.width,res.height,fullScreenSettings.Toggle.isOn);
        }
    }
}