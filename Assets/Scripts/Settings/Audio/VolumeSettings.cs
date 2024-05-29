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

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            myMixer.SetFloat("MusicMixer", Mathf.Log10(volume) * 20f);
        }
        
        public void SetSfxVolume()
        {
            float volume = sfxSlider.value;
            myMixer.SetFloat("SFXMixer", Mathf.Log10(volume) * 20f);
        }
        
    }
}