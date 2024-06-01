using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

namespace MapElements
{
    public class RatController : MonoBehaviour
    {
        [SerializeField] private float minFreq = 5f;
        [SerializeField] private float maxFreq = 10f;

        [Header("Rat")]
        [SerializeField] private float disappearRatAfterSeconds = 3f;
        [SerializeField] private SplineAnimate splineAnimate;
        private Coroutine _coroutine;
        
        private void Start()
        {
            StartCoroutine(StartRat());
        }

        private IEnumerator StartRat()
        {
            while (true)
            {
                float freq = Random.Range(minFreq, maxFreq);
                _coroutine = StartCoroutine(CountDownTime(freq));
                StartCoroutine(DisappearRat());

                yield return new WaitUntil(() => _coroutine == null);
                splineAnimate.gameObject.SetActive(true);
                splineAnimate.ElapsedTime = 0f;
                splineAnimate.Play();

                yield return null;
            }
        }

        private IEnumerator DisappearRat()
        {
            float timeLeft = disappearRatAfterSeconds;
            while (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                yield return null;
            }
            splineAnimate.gameObject.SetActive(false);
        }
        
        private IEnumerator CountDownTime(float timeLeft)
        {
            while (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                yield return null;
            }

            _coroutine = null;
            yield return null;
        }
    }
}