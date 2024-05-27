using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}