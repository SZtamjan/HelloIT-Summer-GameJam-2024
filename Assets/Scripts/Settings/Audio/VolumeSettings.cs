using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Settings.Audio
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer myMixer;

        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Start()
        {
            SetSavedVolumes();
        }

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            myMixer.SetFloat("MusicMixer", Mathf.Log10(volume) * 20f);
            PlayerPrefs.SetFloat("MusicVolume",volume);
        }
        
        public void SetSfxVolume()
        {
            float volume = sfxSlider.value;
            myMixer.SetFloat("SFXMixer", Mathf.Log10(volume) * 20f);
            PlayerPrefs.SetFloat("SFXVolume",volume);
        }

        private void SetSavedVolumes()
        {
            float sfxVol = PlayerPrefs.GetFloat("SFXVolume");
            float musicVol = PlayerPrefs.GetFloat("MusicVolume");
            
            myMixer.SetFloat("SFXMixer", Mathf.Log10(sfxVol) * 20f);
            myMixer.SetFloat("MusicMixer", Mathf.Log10(musicVol) * 20f);

            sfxSlider.value = sfxVol;
            musicSlider.value = musicVol;
        }
    }
}