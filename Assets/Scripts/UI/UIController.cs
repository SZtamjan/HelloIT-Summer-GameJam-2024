using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Economy;
using UnityEngine.UI;
using Class;
using Crafting;
using static Class.SkladnikiClass;
using NaughtyAttributes;

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

        [Header("ActualShop")]
        [SerializeField] public GameObject ActualShop;

        [SerializeField] public List<Button> buttony;

        [SerializeField] public List<TextMeshProUGUI> buttonyText;

        [SerializeField] public List<TextMeshProUGUI> tytuly;

        #endregion Vars

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartNames();
        }

        #region Interactions

        public void SwitchInteractInfoDisplay(bool value)
        {
            if (!value)
            {
                interactInfoText.text = "";
            }
        }

        public void SwitchInteractInfoDisplay(string value)
        {
            interactInfoText.text = $"Press E to interact \n {value}";
        }

        #endregion Interactions

        #region Economy Warnings

        public void EconomyUpdateResources(ResourcesStruct msg)
        {
            cash.text = msg.Cash.ToString();
        }

        public void ShowEconomyWarning(string msg)
        {
            economyWarningText.text = msg;

            if (warningMessageCor == null)
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

        #endregion Economy Warnings

        #region buying

        public void BuySkladnik(int value)
        {
            EconomyResources.Instance.BuySkladnik(value);
            // ewentualnie jakiś hajs tu albo w linijcie wyżej
            UpdateButtons();
        }

        private void StartNames()
        {
            string[] skladnikiNames = Enum.GetNames(typeof(Skladniki));
            for (int i = 0; i < tytuly.Count; i++)
            {
                tytuly[i].text = skladnikiNames[i];
            }
            UpdateButtons();
        }

        public void UpdateButtons()
        {
            string[] skladnikiNames = Enum.GetNames(typeof(Skladniki));
            for (int i = 0; i < tytuly.Count; i++)
            {
                if (SkladnikController.Instance.ReturnIlosc(i) > 4)
                {
                    buttony[i].interactable = false;
                }
                else
                {
                    buttony[i].interactable = true;
                }

                buttonyText[i].text = $"kup {SkladnikController.Instance.ReturnIlosc(i)}/5";
            }
        }

        #endregion buying

        [Button]
        public void WylaczSklep()
        {
            ActualShop.SetActive(false);
        }
    }
}