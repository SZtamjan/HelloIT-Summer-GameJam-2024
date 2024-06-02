using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class RatAudio : MonoBehaviour
    {
        [SerializeField] private float minDelay = .1f;
        [SerializeField] private float maxDelay = 2f;
        
        private AudioObj _audioObj;

        private void Start()
        {
            TryGetComponent(out AudioObj audioObj);
            if (audioObj == null)
            {
                Debug.LogError("BRAK SKRYPTU AudioObj");
                return;
            }
            _audioObj = audioObj;
            
        }

        private void OnEnable()
        {
            if(_audioObj != null && _audioObj.MyAudioSource != null)
            {
                _audioObj.MyAudioSource.clip = _audioObj.sound[0].clip;
                _audioObj.MyAudioSource.PlayDelayed(Random.Range(minDelay,maxDelay));
            }
        }

        private void OnDisable()
        {
            _audioObj.MyAudioSource.Stop();
        }
    }
}