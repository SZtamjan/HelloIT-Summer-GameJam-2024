using Player;
using Player.Interact.InteractBehaviors.Fill;
using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.PickAndPlace
{
    public class PickMedicine : MonoBehaviour
    {
        private Transform _pickUpSpot;

        private void Start()
        {
            _pickUpSpot = PlayerManager.Instance.PickUpSpot;
        }

        public void PickThisUp()
        {
            if (_pickUpSpot.childCount > 0) { return; }
            gameObject.layer = 0;
            var NewMidicine = Object.Instantiate(gameObject);
            NewMidicine.transform.position = _pickUpSpot.position;
            NewMidicine.transform.parent = _pickUpSpot;
            NewMidicine.GetComponent<BoxCollider>().enabled = false;
            gameObject.layer = 0;
            StartCoroutine(GetComponent<BottlesFillAnim>().PlayDeFill(gameObject));
        }
    }
}