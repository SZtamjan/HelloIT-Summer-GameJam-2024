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


        public SkladnikiScriptableObject GetSkładnik()
        {
            return skladnik;
        }

        public List<Objaw> GetObjawy()
        {
            return skladnik.objawy;
        }
    }
}