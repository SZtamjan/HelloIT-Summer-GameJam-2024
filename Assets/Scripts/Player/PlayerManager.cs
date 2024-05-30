using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        [SerializeField] private Transform pickUpSpot;

        public Transform PickUpSpot => pickUpSpot;

        private void Awake()
        {
            Instance = this;
        }
    }
}