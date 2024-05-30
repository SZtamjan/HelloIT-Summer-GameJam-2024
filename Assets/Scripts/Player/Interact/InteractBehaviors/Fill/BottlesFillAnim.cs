using System;
using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Fill
{
    public class BottlesFillAnim : MonoBehaviour
    {
        private Transform lookAtTarget;

        public float duration = 1f;
        private float currentValue;
        private float elapsedTime = 0f;
        private bool isRunning = false;
        
        //Components
        private FillBottle _fillBottle;

        private void Start()
        {
            lookAtTarget = FillTarget.Instance.gameObject.transform;
            if(lookAtTarget == null) Debug.LogError("BRAK LookAtTarget W PLAYER>MainCamera>PickUpSpot");
            
            TryGetComponent(out FillBottle fillBottle);
            if (fillBottle == null)
            {
                Debug.LogError("USTAW KOMPONENT FillBottle NA ITEM");
                return;
            }
            _fillBottle = fillBottle;
        }

        public IEnumerator PlayFill()
        {
            Debug.LogWarning("Played fill anim");

            //po animacji niech ten item zniknie - to nie ten obiekt
            _fillBottle.FillCor = null;
            yield return null;
        }

        public IEnumerator PlayEmpty()
        {
            Debug.LogWarning("Played emptying anim");
            
            
            
            yield return null;
        }
    }
}