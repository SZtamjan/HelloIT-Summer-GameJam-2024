using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Fill
{
    public class FillTarget : MonoBehaviour
    {
        public static FillTarget Instance;

        private Vector3 originalPos;
        [SerializeField] private Vector3 additionalPos = new Vector3(0f,0f,1f);
        private Coroutine moveCor;

        public Coroutine MoveCor
        {
            get => moveCor;
            set => moveCor = value;
        }


        private void Awake()
        {
            originalPos = transform.localPosition;
            Instance = this;
        }

        public IEnumerator StartMovingTarget(float duration)
        {
            Vector3 startPos = transform.localPosition;
            Vector3 targetPos = additionalPos + startPos;
            float elapsedTime = 0f;
            
            while (Vector3.Distance(targetPos, transform.position) > 0.05f)
            {
                elapsedTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
                
                yield return null;
            }
        }

        public void RestorePos()
        {
            transform.localPosition = originalPos;
        }
    }
}