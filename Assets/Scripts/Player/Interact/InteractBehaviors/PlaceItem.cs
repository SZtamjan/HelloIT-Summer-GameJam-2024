using UnityEngine;

namespace Player.Interact.InteractBehaviors
{
    public class PlaceItem : MonoBehaviour
    {
        private Transform _pickUpSpot;
        [Tooltip("If filled, only filled item will be working")]
        [SerializeField] private GameObject whatItem;
        private void Start()
        {
            _pickUpSpot = PlayerManager.Instance.PickUpSpot;
        }

        public void PlaceMyItem()
        {
            if(_pickUpSpot.childCount <= 0) return;
            
            Transform item = _pickUpSpot.GetChild(0);

            if (whatItem != null)
            {
                if(!CheckIfTheSame(item.gameObject)) return;
            }
            
            item.position = transform.position;
            item.parent = transform;
            item.rotation = Quaternion.Euler(-90,0,0);
        }

        public void PlaceItemAndMakeNotPickable()
        {
            if(_pickUpSpot.childCount <= 0) return;
            
            Transform item = _pickUpSpot.GetChild(0);

            if (whatItem != null)
            {
                if(!CheckIfTheSame(item.gameObject)) return;
            }
            
            PlaceMyItem();
            item.gameObject.layer = LayerMask.NameToLayer("Default");
            item.rotation = Quaternion.Euler(-90,0,0);
        }

        private bool CheckIfTheSame(GameObject item)
        {
            if (ReferenceEquals(item, whatItem))
            {
                return true;
            }

            return false;
        }
        
    }
}