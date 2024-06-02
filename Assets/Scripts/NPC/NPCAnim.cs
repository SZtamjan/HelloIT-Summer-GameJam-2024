using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCAnim : MonoBehaviour
{
    public void Usiadl()
    {
        GameManager.Instance.NpcSatDown = true;
    }

    public void Znikl()
    {
        GameManager.Instance.NpcWentAway = true;
    }
}