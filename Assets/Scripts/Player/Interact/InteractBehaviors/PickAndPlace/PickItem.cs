using UnityEngine;

namespace Player.Interact.InteractBehaviors.PickAndPlace
{
    public class PickItem : MonoBehaviour
    {
        private Transform _pickUpSpot;
        private void Start()
        {
            _pickUpSpot = PlayerManager.Instance.PickUpSpot;
        }
        
        public void PickThisUp()
        {
            transform.position = _pickUpSpot.position;
            transform.parent = _pickUpSpot;
        }
    }
}