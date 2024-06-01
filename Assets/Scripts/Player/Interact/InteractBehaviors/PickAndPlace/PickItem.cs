using Crafting;
using UI;
using UnityEngine;
using static Class.SkladnikiClass;

namespace Player.Interact.InteractBehaviors.PickAndPlace
{
    public class PickItem : MonoBehaviour
    {
        private Transform _pickUpSpot;
        private Skladnik _skladnik;

        private void Start()
        {
            _pickUpSpot = PlayerManager.Instance.PickUpSpot;
            _skladnik = GetComponent<Skladnik>();
        }

        public void PickThisUp()
        {
            if (_pickUpSpot.childCount > 0) { return; }
            if (_skladnik.Ilosc < 1) { return; }
            var copy = Instantiate(gameObject);
            copy.transform.position = _pickUpSpot.position;
            copy.transform.parent = _pickUpSpot;
            copy.GetComponent<Collider>().enabled = false;
            _skladnik.Ilosc = -1;
            UIController.Instance.UpdateButtons();
        }
    }
}