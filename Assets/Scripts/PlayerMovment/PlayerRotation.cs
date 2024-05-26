using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        public float sensX;
        public float sensY;

        public Transform orientation;

        float xRotarion;
        float yRotarion;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotarion += mouseX;
            xRotarion -= mouseY;

            xRotarion = Mathf.Clamp(xRotarion, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotarion, yRotarion, 0);
            orientation.rotation = Quaternion.Euler(0, yRotarion, 0);

        }

    }
}