using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public static NPCController Instance;

    [SerializeField] private NPCBody _pacjentBody;


    [SerializeField] private List<NPCScriptableObject> _kolejka;
    public int kolejkaCount=0;


    [SerializeField] private NPCScriptableObject TestowyPacjent;
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

    [Button]
    private void TestPajcent()
    {
        UpdatePajcent(TestowyPacjent);
    }
    
    public void UpdatePajcent(NPCScriptableObject pacjentInfo)
    {
        _pacjentBody.UpdateMask(pacjentInfo.GetMask());
    }

}
