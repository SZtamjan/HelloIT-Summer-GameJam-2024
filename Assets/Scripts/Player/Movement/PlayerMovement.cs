using System.Collections;
using NaughtyAttributes;
using Player.Interact;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _lookAction;

        private Camera _mainCam;
        [SerializeField] private Transform camTransform;

        private Rigidbody _rb;

        [SerializeField] private float playerSpeed = 5f;
        [SerializeField] private float YawSpeed = 100f;
        [SerializeField] private float PitchSpeed = 100f;
        [SerializeField] private float camSpeed = 500f;
        private bool _mouseRotationIsOn = true;
        private bool _playerMovementIsOn = true;

        #region Properties

        public InputAction MoveActionProperty => _moveAction;

        public bool PlayerMovementIsOn
        {
            get => _playerMovementIsOn;
            set => _playerMovementIsOn = value;
        }

        public bool MouseRotationIsOn
        {
            get => _mouseRotationIsOn;
            set => _mouseRotationIsOn = value;
        }

        #endregion Properties

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            //camTransform = GetComponentInChildren<Camera>().transform;
        }

        private void Start()
        {
            LoadSens();
            _mainCam = Camera.main;
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions.FindAction("Move");
            _lookAction = _playerInput.actions.FindAction("Look");
        }

        private void OnEnable()
        {
            //lookAction.Enable();
        }

        private void OnDisable()
        {
            _lookAction.Disable();
        }

        private void FixedUpdate()
        {
            if (PlayerMovementIsOn)
            {
                MovePlayer();
            }
            if (MouseRotationIsOn)
            {
                YawPlayer();
                PitchPlayer();
            }
        }

        public void LoadSens()
        {
            if (!PlayerPrefs.HasKey("MouseSensitivity"))
            {
                Debug.LogWarning("Fatal error, brak zapisanego sensitivity");
                return;
            }

            YawSpeed = PlayerPrefs.GetFloat("MouseSensitivity");
            PitchSpeed = YawSpeed;
        }

        private void YawPlayer()
        {
            Vector2 lookDelta = _lookAction.ReadValue<Vector2>();
            float yaw = lookDelta.x * YawSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, yaw, 0) * transform.rotation;
        }

        private void PitchPlayer()
        {
            Vector2 lookDelta = _lookAction.ReadValue<Vector2>();
            float currentPitch = camTransform.transform.localRotation.eulerAngles.x;

            float pitch = (lookDelta.y * PitchSpeed * Time.deltaTime * (-1));

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

        private void MovePlayer()
        {
            Vector2 dir = _moveAction.ReadValue<Vector2>();
            Quaternion currRot = transform.rotation;
            _rb.AddForce(currRot * new Vector3(dir.x, 0, dir.y) * (playerSpeed * Time.deltaTime));
        }

        private void Update()
        {
            //Quaternion rot = Quaternion.Slerp(mainCam.transform.rotation, camTransform.rotation, camSpeed * Time.deltaTime);
            Quaternion rot = camTransform.rotation;
            Vector3 pos = camTransform.position;
            _mainCam.transform.SetPositionAndRotation(pos, rot);
        }

        [Button]
        public void Test()
        {
            camTransform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        public void TurnPlayerTowardsNPC()
        {
            //yield return new WaitForSeconds(1f);
            camTransform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Physics.SyncTransforms();
        }
    }
}