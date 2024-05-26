﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Interact
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Transform camObj;
        [SerializeField] private float detectDistance = 2f;

        private InteractableObj _interactableObj;
        private void Update()
        {
            DetectInteractable();
        }

        private bool DetectInteractable()
        {
            RaycastHit hit;
            Physics.Raycast(camObj.position, camObj.forward, out hit, detectDistance);
            
            if(hit.collider == null)
            {
                _interactableObj = null;
                return false;
            }
            
            if (hit.collider.gameObject.TryGetComponent(out InteractableObj interactableObj))
            {
                _interactableObj = interactableObj;
                return true;
            }

            _interactableObj = null;
            return false;
        }

        public void UseInteractable(InputAction.CallbackContext ctx)
        {
            if (!DetectInteractable() || _interactableObj == null) return;
            
            if(ctx.started) _interactableObj.StartThisOnInteract();
        }
    }
}