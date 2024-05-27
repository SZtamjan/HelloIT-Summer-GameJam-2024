using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class EconomyConditions : MonoBehaviour
{
    public static EconomyConditions Instance;

    [Header("Warning Text Settings")]
    public float textFullAlpha = 2f;
    public float fadeDuration = 2f;

    private void Awake()
    {
        Instance = this;
    }
    
    public void NotEnoughResources()
    {
        UIController.Instance.ShowEconomyWarning("Not Enough Resources!1!11!");
        Debug.Log("Not enough resources");
    }
}
