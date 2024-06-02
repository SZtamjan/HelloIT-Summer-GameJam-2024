using System.Collections;
using Gameplay;
using Player.Movement;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Sit
{
    public class ClickDeskButSitDownOnChair : MonoBehaviour
    {
        //Components
        private PlayerManager _playerManager;
        private GameManager _gameManager;

        [SerializeField] private Transform placeToSit;
        [SerializeField] private Vector3 playerSitOffset;

        [Header("Teleport info")] 
        [SerializeField] private float teleportDistance = 2f;
        [SerializeField] private TeleportDirection teleportDirection;
        private void Start()
        {
            _gameManager = GameManager.Instance;
            
            _playerManager = PlayerManager.Instance;
        }

        public void SitDownPlayerOnChair()
        {
            if (_playerManager.AmSitting) return;
            SwitchColliders(false);
            _playerManager.transform.position = placeToSit.position + playerSitOffset;
            Physics.SyncTransforms();
            _playerManager.AmSitting = true;
            
            
            StartCoroutine(TeleportMeOut());
        }

        private IEnumerator TeleportMeOut()
        {
            Vector2 test = new Vector2(0, 0);

            yield return new WaitUntil(() => placeToSit.GetComponent<Collider>().isTrigger);
            StartCoroutine(PlayerManager.Instance.GetComponent<PlayerMovement>().TurnPlayerTowardsNPC());
            
            yield return new WaitUntil(() => _gameManager.GameStates == GameStates.StartGameplay);
            yield return new WaitUntil(() => _playerManager.GetComponent<PlayerMovement>().MoveActionProperty.ReadValue<Vector2>() != test);

            Vector3 pPos = _playerManager.transform.position;
            pPos.y = 0.7f; //Floor fixed height
            switch (teleportDirection)
            {
                case TeleportDirection.forward:
                    pPos.z += teleportDistance;
                    _playerManager.transform.position = pPos;
                    Physics.SyncTransforms();
                    break;
                case TeleportDirection.back:
                    pPos.z -= teleportDistance;
                    _playerManager.transform.position = pPos;
                    Physics.SyncTransforms();
                    break;
                case TeleportDirection.left:
                    pPos.x -= teleportDistance;
                    _playerManager.transform.position = pPos;
                    Physics.SyncTransforms();
                    break;
                case TeleportDirection.right:
                    pPos.x += teleportDistance;
                    _playerManager.transform.position = pPos;
                    Physics.SyncTransforms();
                    break;
                default:
                    Debug.LogError("EXCEPTION");
                    break;
            }

            SwitchColliders(true);
            _playerManager.AmSitting = false;
        }

        private void SwitchColliders(bool value)
        {
            placeToSit.TryGetComponent(out Collider collider);
            if (collider == null)
            {
                Debug.LogWarning("Krzeslo bez collidera");
                return;
            }
            
            _playerManager.TryGetComponent(out Rigidbody rigidbody);
            if (rigidbody == null)
            {
                Debug.LogWarning("Player nie ma Rigidbody");
                return;
            }

            rigidbody.useGravity = value;
            rigidbody.velocity = Vector3.zero;

            collider.isTrigger = !value;
        }
    }
}