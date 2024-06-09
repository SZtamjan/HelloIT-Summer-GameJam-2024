using UnityEngine;

namespace OutlineScripts
{
    public class OutlineSelect : MonoBehaviour
    {
        private OutlineScript outlineComp;

        private void Start()
        {
            outlineComp = GetComponent<OutlineScript>();
        }

        private void OnMouseEnter()
        {
            outlineComp.enabled = true;
        }

        private void OnMouseExit()
        {
            outlineComp.enabled = false;
        }
    }
}
