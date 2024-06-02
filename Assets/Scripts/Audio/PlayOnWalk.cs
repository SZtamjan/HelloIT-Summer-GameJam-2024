using System;
using System.Collections;
using Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class PlayOnWalk : MonoBehaviour
    {
        [SerializeField] private float playWalkAfterDistance = 1f;
        private float walkedDistance = 0f;
        private Vector3 lastPos = new Vector3();

        private GameTimeScaleController _gameTimeScaleController;
        private AudioSource _audioSource;
        private AudioObj _audioObj;

        private void Start()
        {
            TryGetComponent(out AudioSource audioSource);
            if (audioSource == null)
            {
                Debug.LogError("Brak Audio Source");
                return;
            }
            _audioSource = audioSource;

            TryGetComponent(out AudioObj audioObj);
            if (audioObj == null)
            {
                Debug.LogError("BRAK SKRYPTU AudioObj");
                return;
            }
            _audioObj = audioObj;

            GameManager.Instance.TryGetComponent(out GameTimeScaleController gameTimeScaleController);
            if (gameTimeScaleController == null)
            {
                Debug.LogError("Brak gamTimeScaleController");
                return;
            }
            _gameTimeScaleController = gameTimeScaleController;
            
            StartCoroutine(PlayWalkOnWalk());
        }

        private IEnumerator PlayWalkOnWalk()
        {
            while (true)
            {
                yield return new WaitUntil(() => !_gameTimeScaleController.gamePaused);
                
                lastPos = transform.position;
                yield return new WaitForSeconds(0.1f);
                
                walkedDistance += Vector3.Distance(lastPos, transform.position);
                if (walkedDistance > playWalkAfterDistance)
                {
                    walkedDistance = 0f;
                    _audioSource.PlayOneShot(_audioObj.sound[Random.Range(0,_audioObj.sound.Count)].clip);
                }

                yield return null;
            }
        }
    }
}