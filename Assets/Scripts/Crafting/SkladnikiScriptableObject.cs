using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Class.ObjawyClass;
using static Class.SkladnikiClass;

namespace Crafting
{
    [CreateAssetMenu(fileName = "Składnik", menuName = "Crafting/Składnik")]
    public class SkladnikiScriptableObject : ScriptableObject
    {
        public Class.SkladnikiClass.Skladnik nazwa;
        public List<Objaw> objawy;

    }
}