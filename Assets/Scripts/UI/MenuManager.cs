using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        [SerializeField] private GameObject entireMenu;
        
        [Header("Elements")]
        [SerializeField] private GameObject main;
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject audioSettings;
        [SerializeField] private GameObject confirmation;
        [SerializeField] private GameObject backBtn;

        private void Awake()
        {
            Instance = this;
        }

        private void ChangeUI(MenuPos newPos)
        {
            switch (newPos)
            {
                case MenuPos.Main:
                    SwitchMain(true);
                    SwitchSettings(false);
                    SwitchAudioSettings(false);
                    SwitchConfirmation(false);
                    SwitchBackBtn(false);
                    break;
                case MenuPos.Settings:
                    SwitchMain(false);
                    SwitchSettings(true);
                    SwitchAudioSettings(false);
                    SwitchConfirmation(false);
                    SwitchBackBtn(true);
                    break;
                case MenuPos.AudioSettings:
                    SwitchMain(false);
                    SwitchSettings(false);
                    SwitchAudioSettings(true);
                    SwitchConfirmation(false);
                    SwitchBackBtn(true);
                    break;
                case MenuPos.Confirmation:
                    SwitchMain(true);
                    SwitchSettings(false);
                    SwitchAudioSettings(false);
                    SwitchConfirmation(true);
                    SwitchBackBtn(false);
                    break;
                default:
                    Debug.LogWarning("FATAL ERROR :c");
                break;
            }
        }

        #region Menu Elements

        private void SwitchMain(bool value)
        {
            main.SetActive(value);
        }
        
        private void SwitchSettings(bool value)
        {
            settings.SetActive(value);
        }
        
        private void SwitchAudioSettings(bool value)
        {
            audioSettings.SetActive(value);
        }
        
        private void SwitchConfirmation(bool value)
        {
            confirmation.SetActive(value);
        }

        private void SwitchBackBtn(bool value)
        {
            backBtn.SetActive(value);
        }

        public void BackButton()
        {
            if (audioSettings.activeSelf)
            {
                ChangeUI(MenuPos.Settings);
                return;
            }
            
            if(settings.activeSelf)
            {
                ChangeUI(MenuPos.Main);
                return;
            }
        }
        
        #endregion
        
        public void SwitchMenuVis(bool value)
        {
            ChangeUI(MenuPos.Main);
            entireMenu.SetActive(value);
        }

        public void ChangeUIToMain() => ChangeUI(MenuPos.Main);
        public void ChangeUIToSettings() => ChangeUI(MenuPos.Settings);
        public void ChangeUIToAudioSettings() => ChangeUI(MenuPos.AudioSettings);
        public void ChangeUIToConfirmation() => ChangeUI(MenuPos.Confirmation);
    }
}

public enum MenuPos
{
    Main,
    Settings,
    AudioSettings,
    Confirmation
}