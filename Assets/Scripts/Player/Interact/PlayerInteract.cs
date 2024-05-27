﻿using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Interact
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Transform camObj;
        [SerializeField] private float detectDistance = 2f;

        private InteractableObj _interactableObj;
        [SerializeField] private LayerMask interactLayer;
        
        private void Update()
        {
            DetectInteractable();
        }

        private bool DetectInteractable()
        {
            RaycastHit hit;
            Physics.Raycast(camObj.position, camObj.forward, out hit, detectDistance, interactLayer);
            
            if(hit.collider == null)
            {
                UIController.Instance.SwitchInteractInfoDisplay(false);
                _interactableObj = null;
                return false;
            }
            
            if (hit.collider.gameObject.TryGetComponent(out InteractableObj interactableObj))
            {
                UIController.Instance.SwitchInteractInfoDisplay(true);
                _interactableObj = interactableObj;
                return true;
            }

            UIController.Instance.SwitchInteractInfoDisplay(false);
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