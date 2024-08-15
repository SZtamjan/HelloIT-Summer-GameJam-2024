using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Economy;
using UnityEngine.UI;
using Class;
using Crafting;
using Economy.EconomyActions;
using static Class.SkladnikiClass;
using NaughtyAttributes;
using NPC;
using Gameplay;
using UI.Shop;
using UIScripts;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        #region Vars

        public static UIController Instance;

        [SerializeField] private TextMeshProUGUI interactInfoText;

        [SerializeField][Foldout("Economy")] private TextMeshProUGUI economyWarningText;

        [SerializeField][Foldout("Economy")] private float fadeDuration = 2f;
        [SerializeField][Foldout("Economy")] private float textFullAlpha = 2f;
        private Coroutine warningMessageCor;

        [SerializeField][Foldout("Economy Display")] private TextMeshProUGUI cash;
        [SerializeField][Foldout("Economy Display")] private TextMeshProUGUI dayCounter;

        [SerializeField][Foldout("ActualShop")] public GameObject ActualShop;

        [SerializeField][Foldout("ActualShop")] private List<Button> buttony;

        [SerializeField][Foldout("ActualShop")] private List<TextMeshProUGUI> buttonyText;

        [SerializeField][Foldout("ActualShop")] private List<TextMeshProUGUI> tytuly;
        
        [SerializeField][Foldout("Book")] private GameObject book;
        [SerializeField][Foldout("Book")] private PageManager bookPageManager;
        
        [SerializeField][Foldout("Book")] private GameObject newsPaperArea;
        [SerializeField][Foldout("Book")] private PageManager newsPaperPageManager;

        #region podsumowanie

        [SerializeField][Foldout("podsumowanie")] private GameObject _podsumowanieDnia;

        [SerializeField][Foldout("podsumowanie")] private List<TextMeshProUGUI> _PacjeciTytuly;
        [SerializeField][Foldout("podsumowanie")] private List<TextMeshProUGUI> _PacjeciOpisy;

        [SerializeField][Foldout("podsumowanie")] private GameObject _podsumowanieGry;
        [SerializeField][Foldout("podsumowanie")] private TextMeshProUGUI _staty;

        #endregion podsumowanie

        #endregion Vars

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartNames();

            //turn off book
            book.SetActive(false);
            DateUpdate();
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

        public void DateUpdate()
        {
            dayCounter.text = (NPCController.Instance.GetDay() + 1).ToString();
        }

        public void ShowEconomyWarning(string msg)
        {
            economyWarningText.text = msg;
            
            economyWarningText.gameObject.SetActive(true);
            
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
            
            economyWarningText.gameObject.SetActive(false);

            warningMessageCor = null;
        }

        #endregion Economy Warnings

        #region buying

        public void BuySkladnik(int value)
        {
            if (EconomyResources.Instance.BuySkladnik(value))
            {
                UpdateButtons();
            }
        }

        private void StartNames()
        {
            Debug.LogWarning("Wywalono nadpisywanie tytulow w sklepie");
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

        public void OpenBook(List<DoublePage> newPages)
        {
            book.SetActive(true);
            bookPageManager.LoadPages(newPages);
            ReactionToUI.Instance.UnlockAndShowCursor();
            ReactionToUI.Instance.LockMouseAndMovement();
            
            ReactionToUI.Instance.blockers.Add(this.CloseBook);
            //Debug.Log("Dlugosc blockers listy " + ReactionToUI.Instance.blockers.Count);
        }

        public void CloseBook()
        {
            book.SetActive(false);
            ReactionToUI.Instance.LockAndHideCursor();
            
            ReactionToUI.Instance.blockers.Remove(this.CloseBook);
            //Debug.Log("Dlugosc blockers listy " + ReactionToUI.Instance.blockers.Count);

            if (ReactionToUI.Instance.blockers.Count == 0)
            {
                ReactionToUI.Instance.UnlockMouseAndMovement();
            }
        }

        public void OpenNews(List<DoublePage> newPages)
        {
            newsPaperArea.SetActive(true);
            newsPaperPageManager.LoadPages(newPages);
            ReactionToUI.Instance.UnlockAndShowCursor();
            ReactionToUI.Instance.LockMouseAndMovement();
            
            ReactionToUI.Instance.blockers.Add(this.CloseNews);
            //Debug.Log("Dlugosc blockers listy " + ReactionToUI.Instance.blockers.Count);
        }

        public void CloseNews()
        {
            newsPaperArea.SetActive(false);
            ReactionToUI.Instance.LockAndHideCursor();
            
            ReactionToUI.Instance.blockers.Remove(this.CloseNews);
            //Debug.Log("Dlugosc blockers listy " + ReactionToUI.Instance.blockers.Count);

            if (ReactionToUI.Instance.blockers.Count == 0)
            {
                ReactionToUI.Instance.UnlockMouseAndMovement();
            }
        }

        [Button]
        public void WylaczSklep()
        {
            ActualShop.SetActive(false);
            ReactionToUI.Instance.LockAndHideCursor();
            ReactionToUI.Instance.UnlockMouseAndMovement();
        }

        #region podsumowanie

        public void testc()
        {
            WlaczPodsumowanie(true);
        }

        public void WlaczPodsumowanie(bool on)
        {
            if (on)
            {
                ReactionToUI.Instance.UnlockAndShowCursor();
                ReactionToUI.Instance.LockMouseAndMovement();

                _podsumowanieDnia.SetActive(true);
                UpdatePodsumowanie();
            }
            else if (NPCController.Instance.KoniecDni())
            {
                _podsumowanieDnia.SetActive(false);
                _podsumowanieGry.SetActive(true);
                UpdatePodsumowanieGry();
            }
            else
            {
                _podsumowanieDnia.SetActive(false);
                GameManager.Instance.WlaczSklep();
            }
        }

        public void UpdatePodsumowanieGry()
        {
            int iloscPacjentow = 0;
            int iloscZdrowych = 0;
            int iloscChorych = 0;
            foreach (var item in NPCController.Instance.PacjeciWszyscy())
            {
                foreach (var itemm in item.kolejka)
                {
                    iloscPacjentow++;
                    if (itemm.CzyWyleczony())
                    {
                        iloscZdrowych++;
                    }
                    else { iloscChorych++; }
                }
            }
            _staty.text = $"Obsłużyłeś{iloscPacjentow} pajcentów \n Z Tego Wyleczyłeś {iloscZdrowych} \n a zabiłeś {iloscChorych} ";
        }

        public void UpdatePodsumowanie()
        {
            var pajeci = NPCController.Instance.PacjeciZDzisiaj();
            if (pajeci.Count < 5)
            {
                for (int i = pajeci.Count; i < 5; i++)
                {
                    _PacjeciTytuly[i].text = $" ";
                    _PacjeciOpisy[i].text = $" ";
                }
            }
            for (int i = 0; i < pajeci.Count; i++)
            {
                string smierć = pajeci[i].CzyWyleczony() ? "PRZEŻYŁ" : "ZMARŁ";
                _PacjeciTytuly[i].text = $"{pajeci[i].GetName()} - {smierć} - {pajeci[i].GetPieniazki()} ";
                _PacjeciOpisy[i].text = $"Pacjent był chory na {pajeci[i].GetNazwaChoroby()}";
            }
        }

        #endregion podsumowanie
    }
}