using UnityEngine;
using UnityEngine.Events;

namespace Player.Interact
{
    public class InteractableObj : MonoBehaviour
    {
        public UnityEvent interactedWithObj;
        public void StartThisOnInteract()
        {
            Debug.Log("Interacted!");
            interactedWithObj.Invoke();
        }
    }
}