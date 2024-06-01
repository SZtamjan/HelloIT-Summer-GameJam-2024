using System;
using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Fill
{
    public class BottlesFillAnim : MonoBehaviour
    {
        private Transform lookAtTarget;

        public float duration = 1f;

        //Components
        private FillBottle _fillBottle;

        private void Start()
        {
            lookAtTarget = FillTarget.Instance.gameObject.transform;
            if (lookAtTarget == null) Debug.LogError("BRAK LookAtTarget W PLAYER>MainCamera>PickUpSpot");

            TryGetComponent(out FillBottle fillBottle);
            if (fillBottle == null)
            {
                Debug.LogError("USTAW KOMPONENT FillBottle NA ITEM");
                return;
            }
            _fillBottle = fillBottle;
        }

        public IEnumerator PlayFill(float fillValueOfOtherBottle)
        {
            Material liquid = GetComponent<MeshRenderer>().materials[2];

            float endValue = fillValueOfOtherBottle;
            float elapsedTime = 0f;
            float currentValue;

            while (liquid.GetFloat("_Fill") >= 0f)
            {
                elapsedTime += Time.deltaTime;
                currentValue = Mathf.Lerp(0f, endValue, elapsedTime / duration);
                liquid.SetFloat("_Fill", currentValue);
                if (elapsedTime >= duration)
                {
                    currentValue = 0f;
                    break;
                }
                yield return null;
            }

            _fillBottle.FillCor = null;
            yield return null;
        }

        public IEnumerator PlayEmpty(GameObject bottle)
        {
            FillTarget fillTarget = FillTarget.Instance;
            Transform target = fillTarget.transform;

            Material liquid = bottle.GetComponent<MeshRenderer>().materials[2];

            float startValue = liquid.GetFloat("_Fill");
            float elapsedTime = 0f;
            float currentValue;

            fillTarget.MoveCor = StartCoroutine(fillTarget.StartMovingTarget(duration));

            while (liquid.GetFloat("_Fill") >= 0f)
            {
                bottle.transform.LookAt(target);

                elapsedTime += Time.deltaTime;
                currentValue = Mathf.Lerp(startValue, 0f, elapsedTime / duration);
                liquid.SetFloat("_Fill", currentValue);
                if (elapsedTime >= duration)
                {
                    currentValue = 0f;
                    break;
                }
                yield return null;
            }

            if (fillTarget.MoveCor != null)
            {
                StopCoroutine(fillTarget.MoveCor);
                fillTarget.MoveCor = null;
            }
            fillTarget.RestorePos();

            yield return null;
        }

        public IEnumerator PlayDeFill(GameObject bottle)
        {
            Material liquid = bottle.GetComponent<MeshRenderer>().materials[2];

            float startValue = liquid.GetFloat("_Fill");
            float elapsedTime = 0f;
            float currentValue;

            while (liquid.GetFloat("_Fill") >= 0f)
            {
                elapsedTime += Time.deltaTime;
                currentValue = Mathf.Lerp(startValue, 0f, elapsedTime / duration);
                liquid.SetFloat("_Fill", currentValue);
                if (elapsedTime >= duration)
                {
                    currentValue = 0f;
                    break;
                }
                yield return null;
            }

            yield return null;
        }
    }
}