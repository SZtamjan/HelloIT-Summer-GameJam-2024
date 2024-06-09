using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static Class.MaskInfoClass;

namespace NPC
{
    public class NPCBody : MonoBehaviour
    {
        [SerializeField] private GameObject mask;
        [SerializeField] private GameObject body;
        [SerializeField] private AudioSource oddychanieSound;
        private Material bodyMaterial;
        private Collider colider;

        private void Start()
        {
            bodyMaterial = body.GetComponent<SkinnedMeshRenderer>().material;
            colider = GetComponent<Collider>();
            MakeInvisible();
        }

        public void MakeInvisible()
        {
            mask.GetComponent<MeshRenderer>().material.SetFloat("_DitherThreshold", 0);
            bodyMaterial.SetFloat("_DitherThreshold", 0);
            oddychanieSound.volume = 0;
            colider.enabled = false;
        }

        public void UpdateMask(MaskInfo maska)
        {
            mask.GetComponent<MeshFilter>().mesh = maska.mesh;
            mask.GetComponent<MeshRenderer>().material = maska.material;
        }

        public IEnumerator ZmienPrzezroczystosc(float target)
        {
            if (target == 1)
            {
                colider.enabled = true;
            }
            else
            {
                colider.enabled = false;
            }
            float fadeTime = 2f;
            float time = 0f;
            float startDither = bodyMaterial.GetFloat("_DitherThreshold");
            while (time < fadeTime)
            {
                time += Time.deltaTime / fadeTime;
                float ditter = Mathf.SmoothStep(startDither, target, time);
                bodyMaterial.SetFloat("_DitherThreshold", ditter);
                oddychanieSound.volume = ditter;
                mask.GetComponent<MeshRenderer>().material.SetFloat("_DitherThreshold", ditter);
                yield return null;
            }
            bodyMaterial.SetFloat("_DitherThreshold", target);

            yield return null;
        }
    }
}