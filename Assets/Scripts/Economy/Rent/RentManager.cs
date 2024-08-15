using System;
using Economy.EconomyActions;
using UnityEngine;

namespace Economy.Rent
{
    public class RentManager : MonoBehaviour
    {
        public static RentManager Instance;
        
        [SerializeField] private ResourcesStruct rentAmount;

        public ResourcesStruct RentAmount => rentAmount;
        private void Awake()
        {
            Instance = this;
        }

        public bool PayRent()
        {
            int cost = rentAmount.Cash;
            int cash = EconomyResources.Instance.Resources.Cash;
            int left = cash - cost;
            if (left >= 0)
            {
                EconomyOperations.Purchase(rentAmount);
                return true;
            }
            
            return false;
        }
    }
}