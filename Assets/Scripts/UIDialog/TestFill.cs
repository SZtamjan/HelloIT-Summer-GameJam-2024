using System;
using NPC;
using UnityEngine;

namespace UIDialog
{
    public class TestFill : MonoBehaviour
    {
        public static TestFill Instance;
        public NPCScriptableObject npcSO;

        private void Awake()
        {
            Instance = this;
        }

        public void FillChat()
        {
            DialogController.Instance.ProcessThroughChats(npcSO.EntryChatList(),npcSO.NormalChatList(),npcSO.ExitChatList());
        }
        
    }
}