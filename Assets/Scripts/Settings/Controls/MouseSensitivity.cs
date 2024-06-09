using Player.Movement;
using Player;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings.Controls
{
    public class MouseSensitivity : MonoBehaviour
    {
        [SerializeField] private float defaultSensitivity;
        [SerializeField] private Slider sensSlider;
        [SerializeField] private TMP_InputField inputField;

        private void Start()
        {
            LoadSens();
        }

        public void OnSliderChangeSens()
        {
            float sliderSens = sensSlider.value;
            inputField.text = sliderSens.ToString("0.00");

            SaveSens(sliderSens);
        }

        public void OnInputFieldChangeSens()
        {
            string stringSens = inputField.text;
            float sens = float.Parse(stringSens, CultureInfo.InvariantCulture.NumberFormat);

            float adjustedSens = Mathf.Clamp(sens, sensSlider.minValue, sensSlider.maxValue);
            sensSlider.value = adjustedSens;

            inputField.text = adjustedSens.ToString("0.00");

            SaveSens(adjustedSens);
        }

        private void LoadSens()
        {
            float loadedSens = defaultSensitivity;
            if (!PlayerPrefs.HasKey("MouseSensitivity"))
            {
                SetComponents(loadedSens);
                return;
            }

            SetComponents(ReadSens());
        }

        private void SetComponents(float sens)
        {
            sensSlider.value = sens;
            inputField.text = sens.ToString("0.00");
            SaveSens(sens);
        }

        private void SaveSens(float newSens)
        {
            PlayerPrefs.SetFloat("MouseSensitivity", newSens);
            PlayerManager.Instance.GetComponent<PlayerMovement>().LoadSens();
        }

        private float ReadSens()
        {
            return PlayerPrefs.GetFloat("MouseSensitivity");
        }
    }
}