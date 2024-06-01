using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.PickAndPlace
{
    public class PlaceInTrash : MonoBehaviour
    {
        private Transform _pickUpSpot;

        private void Start()
        {
            _pickUpSpot = PlayerManager.Instance.PickUpSpot;
        }

        public void PlaceInTrach()
        {
            if (_pickUpSpot.childCount <= 0) return;

            Destroy(_pickUpSpot.GetChild(0).gameObject);
        }
    }
}