using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Class.ObjawyClass;

namespace Crafting
{
    public class Lek : MonoBehaviour
    {

        [SerializeField] private List<Objaw> objawy;
        public void ClearObjawy()
        {
            objawy.Clear();
        }
        public void AddObjaw(Objaw objaw)
        {
            objawy.Add(objaw);
        }

        public void AddObjawy(List<Objaw> newObjawy)
        {
            objawy.AddRange(newObjawy);
        }

        public List<Objaw> GetObjawy()
        {
            return objawy;
        }
    }
}