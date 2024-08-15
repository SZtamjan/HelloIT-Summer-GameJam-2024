using Class;
using Crafting;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Economy.EconomyActions;
using Unity.VisualScripting;
using UnityEngine;
using static Class.SkladnikiClass;

namespace Crafting
{
    public class SkladnikController : MonoBehaviour
    {
        public static SkladnikController Instance;

        public List<Skladnik> storages;
        public int test = 1;

        private void Awake()
        {
            Instance = this;
        }

        [Button]
        public void Test()
        {
            AddSkladnik(1);
        }

        [Button]
        public void Test1()
        {
            AddSkladnik(-1);
        }

        [Button]
        public void Test2()
        {
            Debug.Log(ReturnIlosc(test));
        }

        public bool AddSkladnik(int num)
        {
            var skladnik = storages.Find(x => x.GetSkładnik().nazwa == (SkladnikiClass.Skladniki)num);
            if (skladnik == null) { return false; }

            if (EconomyOperations.Purchase(skladnik.Cena))
            {
                skladnik.Ilosc = 1;
                return true;
            }
            
            return false;
        }

        public int ReturnIlosc(int num)
        {
            var testo = (SkladnikiClass.Skladniki)num;
            Skladnik skladnik = null;
            Debug.Log(testo);
            foreach (var item in storages)
            {
                if (item.GetSkładnik().nazwa == testo)
                {
                    skladnik = item;
                }
            }

            if (skladnik == null) { return 0; }
            return skladnik.Ilosc;
        }
    }
}