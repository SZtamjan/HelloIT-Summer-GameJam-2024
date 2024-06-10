using NPC;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class DziennikAnonimka : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI opis;
    [SerializeField] private List<NPCScriptableObject> pacjentTeks;

    private void OnEnable()
    {
        if ((NPCController.Instance.GetDay() - 1) > -1)
        {
            opis.text = $"{pacjentTeks[NPCController.Instance.GetDay() - 1].GetGazeta()}";
        }
    }
}