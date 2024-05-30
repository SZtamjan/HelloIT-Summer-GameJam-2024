using System;
using UnityEngine;

namespace Main
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}