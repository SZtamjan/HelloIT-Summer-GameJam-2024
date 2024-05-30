using NPC;
using UnityEngine;

namespace UIDialog
{
    public class TestFill : MonoBehaviour
    {
        public NPCScriptableObject npcSO;

        public void dupa()
        {
            DialogController.Instance.ProcessThroughChats(npcSO.EntryChatList(),npcSO.NormalChatList(),npcSO.ExitChatList());
        }
        
    }
}