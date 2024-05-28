using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public static NPCController Instance;


    [SerializeField] private List<NPCScriptableObject> _kolejka;
    public int kolejkaCount=0;

    private void Awake()
    {
        Instance=this;
    }
    private void Start()
    {
        Debug.LogWarning("Nie ma nigogo w kolejce");
    }

    public void NastepnyPacjent()
    {
        kolejkaCount++;
    }

}
