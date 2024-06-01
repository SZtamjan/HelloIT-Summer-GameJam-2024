using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Class.ObjawyClass;

namespace Crafting
{
    public class Skladnik : MonoBehaviour
    {
        [SerializeField, Required] private SkladnikiScriptableObject skladnik;
        [SerializeField] private int _ilosc = 0;

        private void Start()
        {
            Ilosc = 0;
        }

        public SkladnikiScriptableObject GetSkładnik()
        {
            return skladnik;
        }

        public List<Objaw> GetObjawy()
        {
            return skladnik.objawy;
        }

        public void SetSkladnik(SkladnikiScriptableObject obj)
        {
            skladnik = obj;
        }

        public int Ilosc
        {
            get { return _ilosc; }
            set
            {
                _ilosc += value;
                if (_ilosc > 0)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}