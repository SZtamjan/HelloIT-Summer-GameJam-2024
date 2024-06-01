using Crafting;
using System.Collections;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Fill
{
    public class FillBottle : MonoBehaviour
    {
        //Components
        private Transform _pickUpSpot;

        private BottlesFillAnim _bottlesFillAnim;

        //Vars
        private Coroutine fillCor;

        public Transform PickUpSpot => _pickUpSpot;

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
            var czyToSkladnik = _pickUpSpot.GetChild(0).gameObject.TryGetComponent(out Skladnik notUsed);
            if (!czyToSkladnik) return;
            if (gameObject.GetComponent<MeshRenderer>().materials[2].GetFloat("_Fill") > 0.05f) return; //jezeli butelka jest napelniona to przerwij

            StartCoroutine(FillThisBottleCor());
        }

        private IEnumerator FillThisBottleCor()
        {
            Transform item = _pickUpSpot.GetChild(0);

            fillCor = StartCoroutine(_bottlesFillAnim.PlayFill(item.gameObject.GetComponent<MeshRenderer>().materials[2].GetFloat("_Fill")));
            StartCoroutine(_bottlesFillAnim.PlayEmpty(item.gameObject));

            yield return new WaitUntil(() => fillCor == null);
            SendInfoBetweenObjects(gameObject, item.gameObject);

            yield return new WaitForSeconds(0.1f);
            Destroy(item.gameObject);
        }

        private void SendInfoBetweenObjects(GameObject objOne, GameObject objTwo)
        {
            objOne.GetComponent<Skladnik>().SetSkladnik(objTwo.GetComponent<Skladnik>().GetSkładnik());
            Crafting.CraftingController.Instance.SprawdxCzyMoznaCraftowac();
            //objOne to obecny obiekt, ktory zostaje do craftowania
            //objTwo to obiekt z reki gracza, od ktorego nalezy przeslac info do objOne
            Debug.Log("Przesłano dane");
        }
    }
}