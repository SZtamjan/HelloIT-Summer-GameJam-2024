using System;
using UnityEngine;

namespace Audio
{
    public class PlayThisObjectInLoop : MonoBehaviour
    {
        [Tooltip("Numer audio z listy")]
        [SerializeField] private int songIndex;
        
        [Tooltip("In seconds")]
        [SerializeField] private float frequency = 1f;
        private float timePassed = 0f;

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
        }

        private void Update()
        {
            timePassed += Time.deltaTime;

            if (timePassed >= frequency)
            {
                _audioSource.PlayOneShot(_audioObj.sound[songIndex].clip);
            }
        }
    }
}