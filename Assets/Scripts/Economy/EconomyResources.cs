﻿using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Economy
{
    public class EconomyResources : MonoBehaviour
    {
        public static EconomyResources Instance;
        
        [SerializeField] private ResourcesStruct resourcesStruct;

        public ResourcesStruct Resources => resourcesStruct;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UIController.Instance.EconomyUpdateResources(Resources);
        }
    }
}