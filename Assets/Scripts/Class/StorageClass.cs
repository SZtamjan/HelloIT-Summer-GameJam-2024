using Crafting;
using System.Collections;
using UnityEngine;

namespace Class
{
    public class StorageClass : MonoBehaviour
    {
        [System.Serializable]
        public class Storage
        {
            public SkladnikiScriptableObject skladnik;
            public int ilosc = 0;
        }
    }
}