using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Fill
{
    public class FillBottle : MonoBehaviour
    {
        private Transform _pickUpSpot;
        private BottlesFillAnim _bottlesFillAnim;

        private Coroutine fillCor;

        public Coroutine FillCor
        {
            set => fillCor = value;
        }

        private void Start()
        {
            _pickUpSpot = PlayerManager.Instance.PickUpSpot;
            
            TryGetComponent(out BottlesFillAnim bottleFillAnim);
            if (bottleFillAnim == null)
            {
                Debug.LogError("USTAW KOMPONENT FillBottle NA ITEM");
                return;
            }
            _bottlesFillAnim = bottleFillAnim;
        }

        public void FillThisBottle()
        {
            if (_pickUpSpot.childCount <= 0) return;
            if (gameObject.GetComponent<MeshRenderer>().materials[2].GetFloat("_Fill") > 0.05f) return;
            
            StartCoroutine(FillThisBottleCor());
        }

        private IEnumerator FillThisBottleCor()
        {
            Transform item = _pickUpSpot.GetChild(0);

            fillCor = StartCoroutine(_bottlesFillAnim.PlayFill());
            StartCoroutine(_bottlesFillAnim.PlayEmpty());

            yield return new WaitUntil(() => fillCor == null);
            //przekaz info obiektowi czy cos
        }
    }
}