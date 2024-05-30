using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UIDialog
{
    public class DialogController : MonoBehaviour
    {
        public static DialogController Instance;
        
        [SerializeField] private TextMeshProUGUI displayChat;
        
        [Tooltip("For how long text stays in seconds")] 
        [SerializeField] private float lineLastFor = 3f;

        private InputAction _interactionAction;
        private Coroutine _currChatCor;
        
        public void ProcessThroughChats(List<string> entryChat, List<string> normalChat, List<string> exitChat)
        {
            StartCoroutine(ProcessThroughChatsCor(entryChat, normalChat, exitChat));
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _interactionAction = PlayerManager.Instance.GetComponent<PlayerInput>().actions.FindAction("Interaction");
        }

        private IEnumerator ProcessThroughChatsCor(List<string> entryChat, List<string> normalChat, List<string> exitChat)
        {
            _currChatCor = StartCoroutine(GoThroughChat(entryChat));

            yield return new WaitUntil(() => _currChatCor == null);
            _currChatCor = StartCoroutine(GoThroughChat(normalChat));
            
            yield return new WaitUntil(() => _currChatCor == null);
            //yield return new WaitUntil(() => _currChatCor == null);
            _currChatCor = StartCoroutine(GoThroughChat(exitChat));
            yield return null;
        }

        private IEnumerator GoThroughChat(List<string> currChat)
        {
            foreach (var line in currChat)
            {
                
                displayChat.text = line;
                yield return new WaitUntil(() => Mathf.Approximately(_interactionAction.ReadValue<float>(), 1f));
                yield return new WaitUntil(() => Mathf.Approximately(_interactionAction.ReadValue<float>(), 0f));
                
                //yield return new WaitForSeconds(lineLastFor);
            }

            _currChatCor = null;
            yield return null;
        }
    }
}