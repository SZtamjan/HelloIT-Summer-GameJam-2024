using UnityEngine;

public class OutlineSelect : MonoBehaviour
{
    private Outline outlineComp;

    private void Start()
    {
        outlineComp = GetComponent<Outline>();
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
