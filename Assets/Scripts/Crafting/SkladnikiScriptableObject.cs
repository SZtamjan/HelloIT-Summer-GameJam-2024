using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Class.ObjawyClass;
using static Class.SkladnikiClass;

namespace Crafting
{
    [CreateAssetMenu(fileName = "Składnik", menuName = "Crafting/Składnik", order = 100)]
    public class SkladnikiScriptableObject : ScriptableObject
    {
        public Class.SkladnikiClass.Skladniki nazwa;
        public List<Objaw> objawy;
        public ResourcesStruct cena;
    }
}