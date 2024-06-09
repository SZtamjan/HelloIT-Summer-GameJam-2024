using System;
using Audio;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Interact
{
    public class PlayerInteract : MonoBehaviour
    {
        private Transform _camObj;
        [SerializeField] private float detectDistance = 2f;

        private InteractableObj _interactableObj;
        [SerializeField] private LayerMask interactLayer;

        //Components
        private AudioManagerScript _audioManagerScript;

        private void Awake()
        {
            _camObj = Camera.main.transform;
        }

        private void Start()
        {
            _audioManagerScript = AudioManagerScript.Instance;
        }

        private void Update()
        {
            DetectInteractable();
        }

        private bool DetectInteractable()
        {
            RaycastHit hit;
            
            if (Physics.Raycast(_camObj.position, _camObj.forward, out hit, detectDistance, interactLayer))
            {
                if (_interactableObj != null && !_interactableObj.dwa)
                {
                    _interactableObj.LookingAtMe();
                    _interactableObj.dwa = true;
                }
            }
            else
            {
                if (_interactableObj != null)
                {
                    _interactableObj.LookingAtMe();
                    _interactableObj.dwa = false;
                }
                _interactableObj = null;
            }

            if (hit.collider == null)
            {
                UIController.Instance.SwitchInteractInfoDisplay(false);
                _interactableObj = null;
                return false;
            }

            if (hit.collider.gameObject.TryGetComponent(out InteractableObj interactableObj))
            {
                UIController.Instance.SwitchInteractInfoDisplay(true);
                UIController.Instance.SwitchInteractInfoDisplay(interactableObj.CoToJest);
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

            //click audio
            //_audioManagerScript.PlaySFXOneShot("nice");

            if (ctx.started) _interactableObj.StartThisOnInteract();
        }
    }
}