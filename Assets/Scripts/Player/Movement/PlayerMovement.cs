using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _moveAction;

        [SerializeField] private float playerSpeed = 5f;

        #region Properties

        public InputAction MoveActionProperty => _moveAction;

        #endregion

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions.FindAction("Move");
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            Vector2 dir = _moveAction.ReadValue<Vector2>();

            Quaternion currRot = transform.rotation;
            transform.position += currRot * new Vector3(dir.x, 0, dir.y) * (playerSpeed * Time.deltaTime);
        }
    }
}