using Crafting;
using Economy;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
    public class NPCController : MonoBehaviour
    {
        public static NPCController Instance;

        [SerializeField] private NPCBody _pacjentBody;

        [SerializeField] private List<DaysScriptableObject> _kolejka;
        public int kolejkaCount = -1;
        [SerializeField] private int dayCount = 0;
        [SerializeField] private NPCScriptableObject TestowyPacjent;
        private Animator _anim;

        [Button]
        public void NextDay()
        {
            dayCount++;
            kolejkaCount = -1;
            _pacjentBody.MakeInvisible();
        }

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
        public void NastepnyPacjent()
        {
            kolejkaCount++;
            UpdatePajcent(_kolejka[dayCount].kolejka[kolejkaCount]);
            ChangeVisibility(1);
            _anim.SetTrigger("Start");
        }

        [Button]
        public void KoniecPacjenta()
        {
            EconomyResources.Instance.Resources.Cash += _kolejka[dayCount].kolejka[kolejkaCount].GetPieniazki();
            ChangeVisibility(0);
            _anim.SetTrigger("Stop");
        }

        public void UpdatePajcent(NPCScriptableObject pacjentInfo)
        {
            _pacjentBody.UpdateMask(pacjentInfo.GetMask());
        }

        internal void GiveLek(Lek lekarstwo)
        {
            _kolejka[dayCount].kolejka[kolejkaCount].DejLek(lekarstwo);
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