using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        [SerializeField] private Transform pickUpSpot;
        
        //vars
        private bool _amSitting = false;

        public bool AmSitting
        {
            get => _amSitting;
            set => _amSitting = value;
        }
        
        public Transform PickUpSpot => pickUpSpot;

        private void Awake()
        {
            Instance = this;
        }
    }
}