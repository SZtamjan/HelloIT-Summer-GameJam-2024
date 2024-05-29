using Audio;
using Economy.EconomyActions;
using UnityEngine;

namespace Economy
{
    public class BuyTest : MonoBehaviour
    {
        public ResourcesStruct r;
        public void Buy()
        {
            EconomyOperations.Purchase(r);
        }
    }
}