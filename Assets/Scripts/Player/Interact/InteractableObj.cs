using UnityEngine;
using UnityEngine.Events;

namespace Player.Interact
{
    public class InteractableObj : MonoBehaviour
    {
        public UnityEvent interactedWithObj;
        public UnityEvent lookAtObj;
        public string CoToJest;

        [HideInInspector] public bool dwa = false;
        
        public void StartThisOnInteract()
        {
            Debug.Log("Interacted!");
            interactedWithObj.Invoke();
        }

        public void LookingAtMe()
        {
            Debug.Log("Player just looked at me!");
            lookAtObj.Invoke();
        }
        
    }
}