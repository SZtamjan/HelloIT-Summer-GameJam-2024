using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Crafting
{
    public class CraftingController : MonoBehaviour
    {
        public static CraftingController Instance;



        #region testowe zmienne
        public Skladnik jeden;
        public Skladnik dwa;

        public Lek lek;

        #endregion
        private void Awake()
        {
            Instance = this;
        }

        #region Testowe metody

        [Button]
        public void ZrobLek()
        {

            var a = jeden.GetObjawy();
            var b = dwa.GetObjawy();
            lek.ClearObjawy();
            lek.AddObjawy(a);
            lek.AddObjawy(b);
        }

        #endregion
    }
}