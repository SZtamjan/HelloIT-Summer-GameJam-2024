using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class PlayerRotation : MonoBehaviour
    {
        //private Transform playerBody;
        //private Transform camTransform;

        //public float mouseSens = 500f;
        //private float _xRot = 0f;

        //private InputAction _moveAction;

        //private void Start()
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.None;

        //    camTransform = GetComponentInChildren<Camera>().transform;
        //    playerBody = GetComponent<Transform>();


        //}

        //private void FixedUpdate()
        //{
        //    float mouseX = Input.GetAxisRaw("Mouse X") * mouseSens * Time.deltaTime;
        //    float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSens * Time.deltaTime;

        //    _xRot -= mouseY;
        //    _xRot = Mathf.Clamp(_xRot, -90f, 90f);

        //    camTransform.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
        //    playerBody.Rotate(Vector3.up * mouseX);
        //}

    }
}