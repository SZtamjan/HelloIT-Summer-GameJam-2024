using Crafting;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCController : MonoBehaviour
    {
        public static NPCController Instance;

        [SerializeField] private NPCBody _pacjentBody;

        [SerializeField] private List<NPCScriptableObject> _kolejka;
        public int kolejkaCount = 0;

        [SerializeField] private NPCScriptableObject TestowyPacjent;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Debug.LogWarning("Nie ma nigogo w kolejce");
        }

        [Button]
        public void NastepnyPacjent()
        {
            kolejkaCount++;
            UpdatePajcent(_kolejka[kolejkaCount]);
        }

        [Button]
        private void ZmienMaske()
        {
            UpdatePajcent(TestowyPacjent);
        }

        [Button]
        public void SprawdzChorobe()
        {
            //CraftingController.Instance.ZrobLek();
            //var lek = CraftingController.Instance.lekarstwo.GetObjawy();
            //bool wszytskieObjawy = true;
            //foreach (var objaw in TestowyPacjent.GetObjawy())
            //{
            //    if (!lek.Contains(objaw))
            //    {
            //        wszytskieObjawy = false;
            //    }
            //}

            //TestowyPacjent.SetWyleczonyPacjent(wszytskieObjawy);

            //if (wszytskieObjawy)
            //{
            //    Debug.Log("Sukces");
            //}
            //else
            //{
            //    Debug.Log("Œmieræ");
            //}
        }

        public void UpdatePajcent(NPCScriptableObject pacjentInfo)
        {
            _pacjentBody.UpdateMask(pacjentInfo.GetMask());
        }

        internal void GiveLek(Lek lekarstwo)
        {
            _kolejka[kolejkaCount].DejLek(lekarstwo);
        }
    }
}