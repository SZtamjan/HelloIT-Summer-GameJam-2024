using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UIDialog
{
    public class DialogController : MonoBehaviour
    {
        public static DialogController Instance;
        
        //Components
        private GameManager _gameManager;
        
        [SerializeField] private TextMeshProUGUI displayChat;
        
        [Tooltip("For how long text stays in seconds")] 
        [SerializeField] private float lineLastFor = 3f;

        //Vars
        private InputAction _interactionAction;
        private Coroutine _currChatCor;
        private Coroutine _entireChat;
        private bool _normalChatFinished = false;

        public bool NormalChatFinished
        {
            get => _normalChatFinished;
            private set=> _normalChatFinished = value;
        }

        public void ProcessThroughChats(List<string> entryChat, List<string> normalChat, List<string> exitChat)
        {
            if (_entireChat != null)
            {
                Debug.LogError("URUCHOMIONO DRUGI CZAT, ZANIM SKONCZONO POPRZEDNI");
                Debug.LogError("Przerywanie czatu...");
                return;
            }
            _entireChat = StartCoroutine(ProcessThroughChatsCor(entryChat, normalChat, exitChat));
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _interactionAction = PlayerManager.Instance.GetComponent<PlayerInput>().actions.FindAction("Interaction");
        }

        private IEnumerator ProcessThroughChatsCor(List<string> entryChat, List<string> normalChat, List<string> exitChat)
        {
            _currChatCor = StartCoroutine(GoThroughChat(entryChat));
            StartCoroutine(CheckIfDialogTime());

            yield return new WaitUntil(() => _currChatCor == null);
            yield return new WaitUntil(() => _gameManager.GameStates == GameStates.NPCMainDialog);
            _currChatCor = StartCoroutine(GoThroughChat(normalChat));

            yield return new WaitUntil(() => _currChatCor == null);
            NormalChatFinished = true;
            yield return new WaitUntil(() => _gameManager.GameStates == GameStates.VictoryNPC);
            _currChatCor = StartCoroutine(GoThroughChat(exitChat));

            //empty dialogUI
            displayChat.text = "";
            //restore Vars to default
            NormalChatFinished = false;
            
            _entireChat = null;
            yield return null;
        }

        private IEnumerator GoThroughChat(List<string> currChat)
        {
            yield return new WaitUntil(() => Mathf.Approximately(_interactionAction.ReadValue<float>(), 0f));
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

        private IEnumerator CheckIfDialogTime()
        {
            yield return new WaitUntil(() => _gameManager.GameStates == GameStates.NPCMainDialog);
            
            if (_currChatCor == null) yield break;
            StopCoroutine(_currChatCor);
            _currChatCor = null;
        }
    }
}