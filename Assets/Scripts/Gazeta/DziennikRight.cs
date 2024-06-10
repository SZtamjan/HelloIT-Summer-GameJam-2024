using NPC;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class DziennikRight : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI opis;
    [SerializeField] private List<string> loreThing;

    private void OnEnable()
    {
        if ((NPCController.Instance.GetDay() - 1) > -1)
        {
            opis.text = $"{loreThing[NPCController.Instance.GetDay() - 1]}";
        }
    }
}