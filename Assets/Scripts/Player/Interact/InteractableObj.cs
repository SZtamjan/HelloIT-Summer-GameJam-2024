using UnityEngine;

namespace Player.Interact
{
    public class InteractableObj : MonoBehaviour
    {
        public void StartThisOnInteract()
        {
            Debug.Log("Interacted!");
        }
    }
}