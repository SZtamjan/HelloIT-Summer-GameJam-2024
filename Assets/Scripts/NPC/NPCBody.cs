using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MaskInfoClass;

public class NPCBody : MonoBehaviour
{
    [SerializeField] private MeshFilter MeshFilter;
    [SerializeField] private MeshRenderer MeshRenderer;
       
    public void UpdateMask(MaskInfo maska)
    {
        MeshFilter.mesh = maska.mesh;
        MeshRenderer.material = maska.material;
    }
}
