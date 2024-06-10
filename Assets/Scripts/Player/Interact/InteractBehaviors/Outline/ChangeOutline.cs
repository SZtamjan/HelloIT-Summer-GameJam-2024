using UnityEngine;
using OutlineScripts;

namespace Player.Interact.InteractBehaviors.Outline
{
    public class ChangeOutline : MonoBehaviour
    {
        private OutlineScript _outline;

        private void Start()
        {
            if (!TryGetComponent(out OutlineScript outline))
            {
                Debug.LogError("BRAK KOMPONENTU Outline na obiekcie " + gameObject.name);
                return;
            }

            _outline = outline;

            if (!TryGetComponent(out InteractableObj interactableObj))
            {
                Debug.LogError("BRAK KOMPONENTU InteractableObj na obiekcie " + gameObject.name);
                return;
            }
            interactableObj.lookAtObj.AddListener(SwitchOutlineVis);
        }

        private void SwitchOutlineVis()
        {
            if (_outline.enabled)
            {
                Debug.Log("Włączono");
                _outline.enabled = false;
                return;
            }

            Debug.Log("Wyłączono");
            _outline.enabled = true;
        }
    }
}