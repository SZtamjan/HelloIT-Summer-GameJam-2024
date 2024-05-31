using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Settings.Graphic
{
    public class ResolutionSettings : MonoBehaviour
    {
        private Resolution[] _resolutions;
        private int myResolutionIndex = 0;
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

            bool isResolutionSaved = PlayerPrefs.HasKey("ResolutionW");
            int w = 0;
            int h = 0;
            if (isResolutionSaved)
            {
                w = PlayerPrefs.GetInt("ResolutionW");
                h = PlayerPrefs.GetInt("ResolutionH");
            }
            
            for (int i = 0; i < _resolutions.Length; i++)
            {
                double a = _resolutions[i].refreshRateRatio.value;
                string resolution = _resolutions[i].width + " x " + _resolutions[i].height + " @" + Mathf.RoundToInt((float)a);
                resolutions.Add(resolution);

                if (isResolutionSaved)
                {
                    if (_resolutions[i].width == w &&
                        _resolutions[i].height == h)
                    {
                        myResolutionIndex = i;
                    }
                }
                else
                {
                    if (_resolutions[i].width == Screen.currentResolution.width &&
                        _resolutions[i].height == Screen.currentResolution.height)
                    {
                        myResolutionIndex = i;
                    }
                }
            }
            
            resDropdown.AddOptions(resolutions);
            resDropdown.value = myResolutionIndex;
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
            
            SaveResolution(res.width,res.height);
            Screen.SetResolution(res.width,res.height,fullScreenSettings.Toggle.isOn);
        }

        private void SaveResolution(int w, int h)
        {
            PlayerPrefs.SetInt("ResolutionW", w);
            PlayerPrefs.SetInt("ResolutionH", h);
        }

        private Vector2 ReadResolution()
        {
            return new Vector2(PlayerPrefs.GetInt("ResolutionW"),PlayerPrefs.GetInt("ResolutionH"));
        }
    }
}