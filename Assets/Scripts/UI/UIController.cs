using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        #region Vars

        public static UIController Instance;

        [SerializeField] private TextMeshProUGUI interactInfoText;

        [Header("Economy")]
        [SerializeField] private TextMeshProUGUI economyWarningText;
        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private float textFullAlpha = 2f;
        private Coroutine warningMessageCor;

        [Header("Economy Display")]
        [SerializeField] private TextMeshProUGUI cash;
        
        #endregion
        
        private void Awake()
        {
            Instance = this;
        }

        #region Interactions

        public void SwitchInteractInfoDisplay(bool value)
        {
            interactInfoText.gameObject.SetActive(value);
            interactInfoText.text = "Press E to interact";
        }
        
        public void SwitchInteractInfoDisplay(bool value, string dupsko)
        {
            interactInfoText.gameObject.SetActive(value);
            interactInfoText.text = dupsko;
        }

        #endregion
        

        #region Economy Warnings

        public void EconomyUpdateResources(ResourcesStruct msg)
        {
            cash.text = msg.Cash.ToString();
        }
        
        public void ShowEconomyWarning(string msg)
        {
            economyWarningText.text = msg;
            
            if(warningMessageCor == null)
                warningMessageCor = StartCoroutine(WarningLenght());
        }

        private IEnumerator WarningLenght()
        {
            float currentAlpha;
            float targetAlpha = 0f;
            float elapsedTime = 0f;
        
            Vector2 movePos;
            Canvas parentCanvas = economyWarningText.transform.parent.GetComponent<Canvas>();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);

            economyWarningText.transform.position = parentCanvas.transform.TransformPoint(movePos);
        
            economyWarningText.alpha = 1;
            currentAlpha = economyWarningText.alpha;

            yield return new WaitForSeconds(textFullAlpha);

            while (elapsedTime < fadeDuration)
            {
                float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / fadeDuration);

                economyWarningText.alpha = newAlpha;
                yield return null;

                elapsedTime += Time.deltaTime;
            }

            warningMessageCor = null;
        }

        #endregion
        
    }
}