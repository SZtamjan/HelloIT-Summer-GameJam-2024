using System;
using UnityEngine;

namespace Player.Interact.InteractBehaviors.Outline
{
    public class ChangeOutline : MonoBehaviour
    {
        private global::Outline _outline;

        private void Start()
        {
            if (!TryGetComponent(out global::Outline outline))
            {
                Debug.LogWarning("BRAK KOMPONENTU Outline na obiekcie " + gameObject.name);
                return;
            }

            _outline = outline;
        }

        public void SwitchOutlineVis()
        {
            if (_outline.enabled)
            {
                _outline.enabled = false;
                return;
            }

            _outline.enabled = true;
        }
    }
}