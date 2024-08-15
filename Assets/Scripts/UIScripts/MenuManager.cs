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
        [SerializeField] private GameObject confirmation;
        [SerializeField] private GameObject backBtn;
        [SerializeField] private GameObject creators;

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
                    SwitchConfirmation(false);
                    SwitchBackBtn(false);
                    SwitchCreators(false);
                    break;
                case MenuPos.Settings:
                    SwitchMain(false);
                    SwitchSettings(true);
                    SwitchConfirmation(false);
                    SwitchBackBtn(true);
                    SwitchCreators(false);
                    break;
                case MenuPos.Creators:
                    SwitchMain(false);
                    SwitchSettings(false);
                    SwitchConfirmation(false);
                    SwitchBackBtn(true);
                    SwitchCreators(true);
                    break;
                case MenuPos.Confirmation:
                    SwitchMain(true);
                    SwitchSettings(false);
                    SwitchConfirmation(true);
                    SwitchBackBtn(false);
                    SwitchCreators(false);
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

        private void SwitchConfirmation(bool value)
        {
            confirmation.SetActive(value);
        }

        private void SwitchBackBtn(bool value)
        {
            backBtn.SetActive(value);
        }

        private void SwitchCreators(bool value)
        {
            creators.SetActive(value);
        }

        public void BackButton()
        {
            if(settings.activeSelf)
            {
                ChangeUI(MenuPos.Main);
                return;
            }
            if (creators.activeSelf)
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
        public void ChangeUIToCreators() => ChangeUI(MenuPos.Creators);
        public void ChangeUIToConfirmation() => ChangeUI(MenuPos.Confirmation);
    }
}

public enum MenuPos
{
    Main,
    Settings,
    AudioSettings,
    Confirmation,
    Creators
}