using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManagerScript : MonoBehaviour
    {
        public static AudioManagerScript Instance;
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Clips")] 
        public Sound[] sounds;

        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
        }

        public void PlaySFXOneShot(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            sfxSource.PlayOneShot(s.clip);
        }

        //mozna dorobic metode zeby muzyke odtwarzal
    }
}