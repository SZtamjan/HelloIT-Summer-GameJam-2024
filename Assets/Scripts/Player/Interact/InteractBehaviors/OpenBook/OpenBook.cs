using System.Collections.Generic;
using UI;
using UI.Shop;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.OpenBook
{
    public class OpenBook : MonoBehaviour
    {
        [SerializeField] private List<DoublePage> bookPages;

        public void SendBook()
        {
            UIController.Instance.OpenBook(bookPages);
        }
    }
}