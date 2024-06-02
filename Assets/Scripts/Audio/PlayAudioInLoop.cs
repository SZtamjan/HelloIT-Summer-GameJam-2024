using System;
using Gameplay;
using UnityEngine;

namespace Audio
{
    public class PlayAudioInLoop : MonoBehaviour
    {
        [SerializeField] private bool playOnStart = false;

        private GameTimeScaleController _gameTimeScaleController;
        private AudioObj _audioObj;
        private AudioSource _audioSource;
        
        private void Start()
        {
            TryGetComponent(out AudioObj audioObj);
            if (audioObj == null)
            {
                Debug.LogError("BRAK SKRYPTU AudioObj");
                return;
            }
            _audioObj = audioObj;
            
            TryGetComponent(out AudioSource audioSource);
            if (audioSource == null)
            {
                Debug.LogError("BRAK KOMPONENTU AudioSource");
                return;
            }
            _audioSource = audioSource;

            GameManager.Instance.TryGetComponent(out GameTimeScaleController gameTimeScaleController);
            if (gameTimeScaleController == null)
            {
                Debug.LogError("GameTimeScaleController not found");
                return;
            }
            _gameTimeScaleController = gameTimeScaleController;
            
            if (playOnStart) _audioSource.PlayOneShot(_audioObj.sound[0].clip);
        }

        private void Update()
        {
            if (!_audioSource.isPlaying && !_gameTimeScaleController.gamePaused) _audioSource.PlayOneShot(_audioObj.sound[0].clip);
        }
    }
}