using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Audio
{
    public class AudioObj : MonoBehaviour
    {
        public List<Sound> sound;

        private AudioSource _audioSource;

        public AudioSource MyAudioSource
        {
            get => _audioSource;
        }
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            GameManager.Instance.GetComponent<GameTimeScaleController>().pausedGame.AddListener(PauseAudio);
        }

        private void PauseAudio()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
                return;
            }
            
            _audioSource.UnPause();
        }
    }
}