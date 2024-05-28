using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction lookAction;

        private Transform camTransform;

        private Rigidbody _rb;

        [SerializeField] private float playerSpeed = 5f;
        [SerializeField] private float rotationSpeed = 100f;

        #region Properties

        public InputAction MoveActionProperty => _moveAction;

        #endregion


        public float xd = 0;
        public float xd2 = 0;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            camTransform = GetComponentInChildren<Camera>().transform;
        }

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions.FindAction("Move");
            lookAction = _playerInput.actions.FindAction("Look");
        }

        private void OnEnable()
        {
            //lookAction.Enable();
        }

        private void OnDisable()
        {
            lookAction.Disable();
        }

        private void FixedUpdate()
        {
            MovePlayer();
            LookPlayer();
        }

        private void LookPlayer()
        {
            Vector2 lookDelta = lookAction.ReadValue<Vector2>();
            float yaw = lookDelta.x * rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, yaw, 0) * transform.rotation;
        }

        private void MovePlayer()
        {
            Vector2 dir = _moveAction.ReadValue<Vector2>();

            Quaternion currRot = transform.rotation;
            _rb.AddForce(currRot * new Vector3(dir.x, 0, dir.y) * (playerSpeed * Time.deltaTime));
        }

        private void Update()
        {
            Vector2 lookDelta = lookAction.ReadValue<Vector2>();
            float currentPitch = camTransform.transform.localRotation.eulerAngles.x;

            float pitch = (lookDelta.y * rotationSpeed * Time.deltaTime * (-1));

            xd = currentPitch;
            xd2 = pitch;
            if (currentPitch > 80f && currentPitch < 90f)
            {
                if (pitch > 0f)
                {
                    Debug.Log(2);

                    pitch = 0f;
                }
            }
            else if (currentPitch < 280f && currentPitch > 90f)
            {
                if (pitch < 0f)
                {
                    Debug.Log(1);
                    Debug.Log(currentPitch);
                    Debug.Log(pitch);
                    pitch = 0f;
                }
            }


            camTransform.transform.localRotation = Quaternion.Euler(pitch + currentPitch, 0f, 0f);

        }
    }
}