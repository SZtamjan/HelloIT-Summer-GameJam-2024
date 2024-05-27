using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCList : MonoBehaviour
{
    [SerializeField] private List<NPCScriptableObject> _kolejka;
    public int kolejkaCount=0;

    
    public void NastepnyPacjent()
    {
        kolejkaCount++;
    }

}
