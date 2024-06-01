using Crafting;
using NPC;
using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.PickAndPlace
{
    public class GiveMedicine : MonoBehaviour
    {
        private Transform _pickUpSpot;

        private void Start()
        {
            _pickUpSpot = PlayerManager.Instance.PickUpSpot;
        }

        public void GiveLekarstwo()
        {
            if (_pickUpSpot.childCount <= 0) return;
            var obiekt = _pickUpSpot.GetChild(0).gameObject;
            bool czyToLek = obiekt.TryGetComponent<Lek>(out Lek lekarstwo);
            if (!czyToLek) return;
            NPCController.Instance.GiveLek(lekarstwo);
            Destroy(lekarstwo.gameObject);
        }
    }
}