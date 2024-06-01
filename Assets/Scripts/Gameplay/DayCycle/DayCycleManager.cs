using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace Gameplay.DayCycle
{
    public class DayCycleManager : MonoBehaviour
    {
        public DayTime currentDayTime;

        [SerializeField] private Light light;
        
        [SerializeField] private float candleIntensity = .22f;
        [SerializeField] private float flameSize = .13f;
        
        [SerializeField] private List<GameObject> candles;
        [SerializeField] private List<GameObject> godRays;

        [SerializeField, Foldout("Settings")] private DayCycleSettings morningSettings;
        [SerializeField, Foldout("Settings")] private DayCycleSettings middaySettings;
        [SerializeField, Foldout("Settings")] private DayCycleSettings eveningSettings;
        //zrob duration i wgl zeby mogli se dostosowywac
        
        [Button]
        public void FillDayTime(DayTime newTime)
        {
            if (newTime == currentDayTime) return;
            switch (currentDayTime)
            {
                case DayTime.morning:
                    StartCoroutine(SetupDay(morningSettings));
                    //StartCoroutine(SetCandles(morningSettings.candleOn,morningSettings));
                    break;
                case DayTime.midday:
                    StartCoroutine(SetupDay(middaySettings));
                    //StartCoroutine(SetCandles(middaySettings.candleOn,middaySettings));
                    break;
                case DayTime.evening:
                    StartCoroutine(SetupDay(eveningSettings));
                    //StartCoroutine(SetCandles(eveningSettings.candleOn,eveningSettings));
                    break;
                default:
                    Debug.LogError("Zly stan dnia xd");
                    break;
            }
        }

        private IEnumerator SetupDay(DayCycleSettings daySettings)
        {
            float startIntensity = light.intensity;
            float endIntensity = daySettings.daylightIntensity;
            float intensityDistance = startIntensity - endIntensity;
            float elapsedTime = 0f;
            float currentValue;

            while (intensityDistance > 0f)
            {
                elapsedTime += Time.deltaTime;
                currentValue = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / daySettings.duration);
                light.intensity = currentValue;
                if (elapsedTime >= daySettings.duration)
                {
                    currentValue = endIntensity;
                    light.intensity = currentValue;
                    break;
                }
                
                //if polowa czasu minela
                if (elapsedTime > daySettings.duration/2)
                {
                    SwitchObjects(candles,daySettings.candleOn);
                    SwitchObjects(godRays,daySettings.godRaysOn);
                }
                
                intensityDistance = light.intensity - morningSettings.daylightIntensity;
                yield return null;
            }
        }

        private void SwitchObjects(List<GameObject> objects, bool value)
        {
            foreach (var obj in objects)
            {
                obj.SetActive(value);
            }
        }

        private IEnumerator SetCandles(bool value, DayCycleSettings daySettings)
        {
            if (value)
            {//wlacz swieczki

                float startIntensity = candleIntensity;
                float endIntensity = daySettings.daylightIntensity;
                float elapsedTime = 0f;
                float currentValue;
                float intensityDistance = startIntensity - endIntensity;
            
                while (intensityDistance >= 0f)
                {
                    foreach (var candle in candles)
                    {
                        elapsedTime += Time.deltaTime;
                        currentValue = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / daySettings.duration);
                        VisualEffectAsset visualEffectAsset = new VisualEffectAsset();
                        //visualEffectAsset.GetExposedProperties("FlameSize");
                        
                        ParticleSystem ps = candle.transform.GetChild(1).GetComponent<ParticleSystem>();
                        //ps.isEmitting = value;
                        if (elapsedTime >= daySettings.duration)
                        {
                            currentValue = endIntensity;
                            //candle = currentValue;
                            break;
                        }
                    
                        //intensityDistance = candle - daySettings.daylightIntensity;
                        yield return null;
                    }
                }
            }
            else
            {//wylacz swieczki
                
                yield return null;
            }

            yield return null;
        }
        
    }

    public enum DayTime
    {
        morning,
        midday,
        evening
    }
}