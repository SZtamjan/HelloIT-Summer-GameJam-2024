using System;
using System.Collections;
using Player.Movement;
using UnityEngine;

namespace Player.Interact.InteractBehaviors
{
    public class SitDown : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private float _playerHeight;

        [SerializeField] private TeleportDirection teleportDirection;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _playerHeight = _playerManager.transform.position.y;
        }

        public void SitDownPlayer()
        {
            _playerManager.transform.position = transform.position + new Vector3(0, _playerHeight, 0);
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
                    _playerManager.transform.position = pPos + new Vector3(0, 0, pPos.z + 1f);
                    break;
                case TeleportDirection.back:
                    _playerManager.transform.position = pPos + new Vector3(0, 0, pPos.z - 1f);
                    break;
                default:
                    Debug.LogError("EXCEPTION");
                    break;
            }
            
        }
    }
}

public enum TeleportDirection
{
    forward,
    back
}