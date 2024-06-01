using System;
using UnityEngine;

namespace Gameplay.DayCycle
{
    [Serializable]
    public class DayCycleSettings
    {
        public float duration = 2f;

        public bool candleOn = false;
        public bool godRaysOn = false;

        [Range(0,1)] public float daylightIntensity = 1f;
    }
}