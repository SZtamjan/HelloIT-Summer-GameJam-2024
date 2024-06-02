using System;
using Player;
using Player.Movement;
using UnityEngine;

namespace UI
{
    public class ReactionToUI : MonoBehaviour
    {
        public static ReactionToUI Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void LockMouseAndMovement()
        {
            LockMouse();
            LockMovement();
        }
        
        public void UnlockMouseAndMovement()
        {
            UnlockMouse();
            UnlockMovement();
        }
        
        public void LockMouse()
        {
            PlayerManager.Instance.GetComponent<PlayerMovement>().MouseRotationIsOn = false;
        }

        public void UnlockMouse()
        {
            PlayerManager.Instance.GetComponent<PlayerMovement>().MouseRotationIsOn = true;
        }

        public void LockMovement()
        {
            PlayerManager.Instance.GetComponent<PlayerMovement>().PlayerMovementIsOn = false;
        }

        public void UnlockMovement()
        {
            PlayerManager.Instance.GetComponent<PlayerMovement>().PlayerMovementIsOn = true;
        }
        
        public void LockAndHideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void UnlockAndShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}