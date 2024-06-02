using UnityEngine;

namespace Player.Movement
{
    public class MoveCam : MonoBehaviour
    {
        public Transform CameraPosition;
        void LateUpdate()
        {
            transform.position = CameraPosition.position;
        }
    }
}