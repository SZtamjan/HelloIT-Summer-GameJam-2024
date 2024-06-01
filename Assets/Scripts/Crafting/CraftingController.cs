using Class;
using NaughtyAttributes;
using Player.Interact.InteractBehaviors.Fill;
using UnityEngine;
using static Class.ObjawyClass;

namespace Crafting
{
    public class CraftingController : MonoBehaviour
    {
        public static CraftingController Instance;

        [SerializeField] private Skladnik _butelka1;
        [SerializeField] private Skladnik _butelka2;

        [SerializeField] private Lek lekarstwo;

        [SerializeField] private SkladnikiScriptableObject _nic;

        private void Awake()
        {
            Instance = this;
        }

        [Button]
        public void ZrobLek()
        {
            var a = _butelka1.GetObjawy();
            var b = _butelka2.GetObjawy();
            lekarstwo.ClearObjawy();
            lekarstwo.AddObjawy(a);
            lekarstwo.AddObjawy(b);
        }

        public void SprawdxCzyMoznaCraftowac()
        {
            if (_butelka1.GetSkładnik().nazwa != Class.SkladnikiClass.Skladniki.None && _butelka2.GetSkładnik().nazwa != Class.SkladnikiClass.Skladniki.None)
            {
                Debug.Log("Można craftować");
                CraftujPote();
            }
            else
            {
                Debug.Log("Jeszcze nie można");
            }
        }

        public void CraftujPote()
        {
            Color hdrColor = GetRandomHDRColor(2.0f);
            lekarstwo.GetComponent<MeshRenderer>().materials[2].SetColor("_Side_Color", hdrColor);
            lekarstwo.GetComponent<MeshRenderer>().materials[2].SetColor("_Top_Color", hdrColor);
            StartCoroutine(_butelka1.GetComponent<BottlesFillAnim>().PlayDeFill(_butelka1.gameObject));
            StartCoroutine(_butelka2.GetComponent<BottlesFillAnim>().PlayDeFill(_butelka2.gameObject));
            StartCoroutine(lekarstwo.GetComponent<BottlesFillAnim>().PlayFill(0.3f));
            lekarstwo.AddObjawy(_butelka1.GetObjawy());
            lekarstwo.AddObjawy(_butelka2.GetObjawy());
            _butelka1.SetSkladnik(_nic);
            _butelka2.SetSkladnik(_nic);

            lekarstwo.gameObject.layer = 6;
        }

        public static Color GetRandomHDRColor(float intensity = 1.0f)
        {
            // Generate random color components
            float r = Random.value;
            float g = Random.value;
            float b = Random.value;

            // Create the color with specified intensity
            Color randomColor = new Color(r, g, b) * intensity;

            // Return the HDR color
            return randomColor;
        }
    }
}