using Crafting;
using Economy;
using Gameplay;
using NaughtyAttributes;
using System.Collections.Generic;
using Gameplay.DayCycle;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
    public class NPCController : MonoBehaviour
    {
        public static NPCController Instance;

        [SerializeField] private NPCBody _pacjentBody;

        [SerializeField] private List<DaysScriptableObject> _kolejka;
        public int kolejkaCount = 0;
        [SerializeField] private int dayCount = 0;
        [SerializeField] private NPCScriptableObject TestowyPacjent;
        private Animator _anim;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Debug.LogWarning("Nie ma nigogo w kolejce");
            _anim = _pacjentBody.GetComponent<Animator>();
        }

        [Button]
        public void NextDay()
        {
            dayCount++;
            kolejkaCount = 0;
            _pacjentBody.MakeInvisible();
        }

        [Button]
        public void NastepnyPacjent()
        {
            Debug.Log("Pacjent nr: " + kolejkaCount);
            Debug.Log("Z day nr: " + dayCount);
            UpdatePajcent(_kolejka[dayCount].kolejka[kolejkaCount]);
            DayCycleManager.Instance.FillDayTime(_kolejka[dayCount].kolejka[kolejkaCount].setDayTime);
            ChangeVisibility(1);
            _anim.SetTrigger("Start");
        }

        [Button]
        public void KoniecPacjenta()
        {
            EconomyResources.Instance.Resources.Cash += _kolejka[dayCount].kolejka[kolejkaCount].GetPieniazki();
            ChangeVisibility(0);
            _anim.SetTrigger("Stop");
            kolejkaCount++;
        }

        public void ResetAllPacjenci()
        {
            foreach (var dni in _kolejka)
            {
                foreach (var pajcent in dni.kolejka)
                {
                    pajcent.SetWyleczonyPacjent(false);
                }
            }
        }

        public bool KoniecDni()
        {
            if (dayCount >= _kolejka.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KonieKolejki()
        {
            if (kolejkaCount >= _kolejka[dayCount].kolejka.Count)
            {
                return true;
            }
            return false;
        }

        public List<NPCScriptableObject> PacjeciZDzisiaj()
        {
            return _kolejka[dayCount].kolejka;
        }

        public List<DaysScriptableObject> PacjeciWszyscy()
        {
            return _kolejka;
        }

        public NPCScriptableObject GetCurrnetPacjent()
        {
            return _kolejka[dayCount].kolejka[kolejkaCount];
        }

        public void UpdatePajcent(NPCScriptableObject pacjentInfo)
        {
            _pacjentBody.UpdateMask(pacjentInfo.GetMask());
        }

        internal void GiveLek(Lek lekarstwo)
        {
            _kolejka[dayCount].kolejka[kolejkaCount].DejLek(lekarstwo);
            GameManager.Instance.ChangeGameState(GameStates.VictoryNPC);
        }

        private void ChangeVisibility(float vis)
        {
            StartCoroutine(_pacjentBody.ZmienPrzezroczystosc(vis));
        }

        [Button]
        public void test0()
        {
            ChangeVisibility(0);
        }

        [Button]
        public void test1()
        {
            ChangeVisibility(1);
        }
    }
}