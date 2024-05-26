using UnityEngine;

namespace Player.Movement
{
    public class MoveCam : MonoBehaviour
    {
        public Transform CameraPosition;
        void Update()
        {
            transform.position = CameraPosition.position;
        }
    }
}