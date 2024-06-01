using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "Kolejka", menuName = "NPC`s/Kolejka", order = 100)]
    public class DaysScriptableObject : ScriptableObject
    {
        public List<NPCScriptableObject> kolejka;
    }
}