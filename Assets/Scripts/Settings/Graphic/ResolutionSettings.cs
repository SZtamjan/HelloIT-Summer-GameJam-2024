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
            LoadRes();
        }

        private void SetUpDropdown()
        {
            _resolutions = Screen.resolutions;
            
            resDropdown.ClearOptions();

            List<string> resolutions = new List<string>();
            
            for (int i = 0; i < _resolutions.Length; i++)
            {
                double a = _resolutions[i].refreshRateRatio.value;
                string resolution = _resolutions[i].width + " x " + _resolutions[i].height + " @" + Mathf.RoundToInt((float)a);
                resolutions.Add(resolution);
                
                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    myResolutionIndex = i;
                }
            }
            
            resDropdown.AddOptions(resolutions);
            resDropdown.value = myResolutionIndex; //to trzeba gdzies wywalic bo nie bedzie dobrze dzialac
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

        private void LoadRes()
        {
            //sprawdz czy hasKey
        }
        
        private void SaveResolution(float w, float h)
        {
            PlayerPrefs.SetFloat("ResolutionW", w);
            PlayerPrefs.SetFloat("ResolutionH", h);
        }

        private Vector2 ReadResolution()
        {
            return new Vector2(PlayerPrefs.GetFloat("ResolutionW"),PlayerPrefs.GetFloat("ResolutionH"));
        }
    }
}