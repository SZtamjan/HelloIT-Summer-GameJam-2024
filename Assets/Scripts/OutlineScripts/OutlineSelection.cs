using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OutlineScripts
{
    public class OutlineSelection : MonoBehaviour
    {
        private Transform highlight;
        private Transform selection;
        private RaycastHit raycastHit;

        void Update()
        {
            // Highlight
            if (highlight != null)
            {
                highlight.gameObject.GetComponent<OutlineScript>().enabled = false;
                highlight = null;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!EventSystem.current.IsPointerOverGameObject() &&
                Physics.Raycast(ray,
                    out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
            {
                highlight = raycastHit.transform;
                if (highlight.CompareTag("Selectable") && highlight != selection)
                {
                    if (highlight.gameObject.GetComponent<OutlineScript>() != null)
                    {
                        highlight.gameObject.GetComponent<OutlineScript>().enabled = true;
                    }
                    else
                    {
                        OutlineScript outline = highlight.gameObject.AddComponent<OutlineScript>();
                        outline.enabled = true;
                        highlight.gameObject.GetComponent<OutlineScript>().OutlineColor = Color.magenta;
                        highlight.gameObject.GetComponent<OutlineScript>().OutlineWidth = 7.0f;
                    }
                }
                else
                {
                    highlight = null;
                }
            }

            // Selection
            if (Input.GetMouseButtonDown(0))
            {
                if (highlight)
                {
                    if (selection != null)
                    {
                        selection.gameObject.GetComponent<OutlineScript>().enabled = false;
                    }

                    selection = raycastHit.transform;
                    selection.gameObject.GetComponent<OutlineScript>().enabled = true;
                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        selection.gameObject.GetComponent<OutlineScript>().enabled = false;
                        selection = null;
                    }
                }
            }
        }

    }
}
