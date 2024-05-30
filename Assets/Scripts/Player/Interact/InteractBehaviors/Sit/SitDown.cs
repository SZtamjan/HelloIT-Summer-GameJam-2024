using System.Collections;
using Player.Movement;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Sit
{
    public class SitDown : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private float _playerHeight;

        [Header("Teleport info")] 
        [SerializeField] private float teleportDistance = 2f;
        [SerializeField] private TeleportDirection teleportDirection;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _playerHeight = _playerManager.transform.position.y;
        }

        public void SitDownPlayer()
        {
            _playerManager.transform.position = transform.position + new Vector3(0, _playerHeight, 0);
            Physics.SyncTransforms();
            StartCoroutine(TeleportMeOut());
        }

        private IEnumerator TeleportMeOut()
        {
            Vector2 test = new Vector2(0, 0);
            yield return new WaitUntil(() => _playerManager.GetComponent<PlayerMovement>().MoveActionProperty.ReadValue<Vector2>() != test);

            Vector3 pPos = _playerManager.transform.position;
            switch (teleportDirection)
            {
                case TeleportDirection.forward:
                    pPos.z += teleportDistance;
                    _playerManager.transform.position = pPos;
                    break;
                case TeleportDirection.back:
                    pPos.z -= teleportDistance;
                    _playerManager.transform.position = pPos;
                    break;
                default:
                    Debug.LogError("EXCEPTION");
                    break;
            }
            
        }
    }

    public enum TeleportDirection
    {
        forward,
        back
    }
}