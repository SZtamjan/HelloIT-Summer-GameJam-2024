using Class;
using Crafting;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Class.SkladnikiClass;

namespace Crafting
{
    public class SkladnikController : MonoBehaviour
    {
        public List<Skladnik> storages;
        public int test = 1;

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

        public void AddSkladnik(int num)
        {
            var skladnik = storages.Find(x => x.GetSk³adnik().nazwa == (SkladnikiClass.Skladniki)num);
            if (skladnik == null) { return; }
            skladnik.Ilosc = 1;
        }

        public int ReturnIlosc(int num)
        {
            var testo = (SkladnikiClass.Skladniki)num;
            Skladnik skladnik = null;
            Debug.Log(testo);
            foreach (var item in storages)
            {
                if (item.GetSk³adnik().nazwa == testo)
                {
                    skladnik = item;
                }
            }

            if (skladnik == null) { return 0; }
            return skladnik.Ilosc;
        }
    }
}