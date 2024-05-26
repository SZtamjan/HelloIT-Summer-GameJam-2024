using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;

        [SerializeField] private TextMeshProUGUI interactInfo;

        private void Awake()
        {
            Instance = this;
        }

        public void SwitchInteractInfoDisplay()
        {
            if (interactInfo.gameObject.activeSelf)
            {
                interactInfo.gameObject.SetActive(false);
                return;
            }
            
            interactInfo.gameObject.SetActive(true);
        }
        
    }
}