using System.Collections;
using Gameplay;
using UnityEngine;

namespace Audio
{
    public class PlayOnCall : MonoBehaviour
    {
        private GameTimeScaleController _gameTimeScaleController;
        private AudioSource _audioSource;
        private AudioObj _audioObj;
        private Coroutine _coroutine;
        
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
        }
        
        public void PlayRandomSoundFromMe()
        {
            if(!_audioSource.isPlaying) _audioSource.PlayOneShot(_audioObj.sound[Random.Range(0,_audioObj.sound.Count)].clip);
        }

        public void StartPlayRandomSoundFromMeConstant()
        {
            _coroutine = StartCoroutine(PlayRandomSoundFromMeConstantCor());
        }

        public void StopPlayRandomSoundFromMeConstant()
        {
            if(_coroutine == null) return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator PlayRandomSoundFromMeConstantCor()
        {
            while (true)
            {
                yield return new WaitUntil(() => !_audioSource.isPlaying);
                PlayRandomSoundFromMe();
                yield return null;
            }
        }
        
        private IEnumerator StopRandomSoundFromMeConstantCor()
        {
            
            yield return null;
        }
    }
}