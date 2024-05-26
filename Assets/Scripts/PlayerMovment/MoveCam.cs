using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform CameraPosition;
    void Update()
    {
        transform.position = CameraPosition.position;
    }
}
