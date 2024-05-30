using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Class.ChorobaClass;
using static Class.ObjawyClass;

namespace Crafting
{
    [CreateAssetMenu(fileName = "Horoba", menuName = "Crafting/Horoba", order = 100)]
    public class ChorobaScriptableObject : ScriptableObject
    {
        public Choroba nazwa;
        public List<Objaw> objawy;
    }
}