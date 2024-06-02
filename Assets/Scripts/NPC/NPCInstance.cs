using System;
using UnityEngine;

namespace NPC
{
    public class NPCInstance : MonoBehaviour
    {
        public static NPCInstance Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}