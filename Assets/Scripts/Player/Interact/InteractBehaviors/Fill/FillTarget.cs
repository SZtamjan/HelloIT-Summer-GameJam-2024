using UnityEngine;

namespace Player.Interact.InteractBehaviors.Fill
{
    public class FillTarget : MonoBehaviour
    {
        public static FillTarget Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}