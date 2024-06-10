using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchComponentOnLook : MonoBehaviour
{
    public Camera playerCamera; // Kamera gracza
    public float maxDistance = 10f; // Maksymalna odleg³oœæ, na jak¹ kamera mo¿e wykrywaæ obiekty
    public LayerMask interactableLayer; // Warstwa interaktywnych obiektów
    public string interactableTag = "outline"; // Tag interaktywnych obiektów
    public Material outlineMaskMaterial; // Materia³ maskuj¹cy kontur
    public Material outlineFillMaterial; // Materia³ wype³niaj¹cy kontur

    private GameObject lastHitObject = null;
    private Dictionary<Renderer, List<Material>> originalMaterials = new Dictionary<Renderer, List<Material>>();

    private void Update()
    {
        CheckForInteractableObject();
    }

    private void CheckForInteractableObject()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag(interactableTag))
            {
                if (hitObject != lastHitObject)
                {
                    if (lastHitObject != null)
                    {
                        RemoveOutlineMaterials();
                    }

                    lastHitObject = hitObject;
                    AddOutlineMaterials();
                }
            }
            else if (lastHitObject != null)
            {
                RemoveOutlineMaterials();
                lastHitObject = null;
            }
        }
        else
        {
            if (lastHitObject != null)
            {
                RemoveOutlineMaterials();
                lastHitObject = null;
            }
        }
    }

    private void AddOutlineMaterials()
    {
        if (lastHitObject == null) return;

        Renderer[] renderers = lastHitObject.GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            var materials = renderer.sharedMaterials.ToList();

            if (!originalMaterials.ContainsKey(renderer))
            {
                originalMaterials[renderer] = new List<Material>(materials);
            }

            if (!materials.Contains(outlineMaskMaterial) && !materials.Contains(outlineFillMaterial))
            {
                materials.Add(outlineMaskMaterial);
                materials.Add(outlineFillMaterial);
                renderer.materials = materials.ToArray();
            }
        }
    }

    private void RemoveOutlineMaterials()
    {
        foreach (var renderer in originalMaterials.Keys)
        {
            renderer.materials = originalMaterials[renderer].ToArray();
        }

        originalMaterials.Clear();
    }
}
