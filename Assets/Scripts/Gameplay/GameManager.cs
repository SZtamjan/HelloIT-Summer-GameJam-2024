﻿using System;
using System.Collections;
using NaughtyAttributes;
using Player;
using Player.Movement;
using UIDialog;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private GameStates _gameStates;

        //Components
        [SerializeField] private DialogController dialogController;
        private PlayerManager _playerManager;
        private PlayerMovement _playerMovement;

        //Vars
        private bool npcSatDown = false;
        private bool npcWentAway = false;

        #region Properties
        
        public GameStates GameStates
        {
            get => _gameStates;
            private set => _gameStates = value;
        }
        public bool NpcWentAway
        {
            get => npcWentAway;
            set => npcWentAway = value;
        }
        public bool NpcSatDown
        {
            get => npcSatDown;
            set => npcSatDown = value;
        }

        #endregion
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _playerManager = PlayerManager.Instance;

            _playerManager.TryGetComponent(out PlayerMovement playerMovement);
            if (playerMovement == null)
            {
                Debug.LogError("Brak PlayerMovement na obiekcie z PlayerManager");
                return;
            }

            _playerMovement = playerMovement;

            StartGameMethod();
        }

        private void StartGameMethod()
        {
            ChangeGameState(GameStates.StartGame);
        }
        
        public void ChangeGameState(GameStates newState)
        {
            GameStates = newState;
            Debug.Log("Current state: " + newState);
            switch (newState)
            {
                case GameStates.StartGame:
                    WaitForPlayerToSitAndLockPlayer(GameStates.StartNextNPC);
                    break;
                case GameStates.StartNextNPC:
                    StartNPC(); //NPC WalksIn - EMPTY Method
                    ChangeGameState(GameStates.StartChat); //StartChat
                    break;
                case GameStates.StartChat:
                    StartChat(); //zczytaj z npc czat z jego SO
                    //WaitForNPCToSit(); //Czekaj aż npc usiądzie
                    WaitForPlayerAndNPCToSitDownAndLockPlayer();
                    break;
                case GameStates.NPCMainDialog:
                    //Jak ten state sie zaladuje, to automatycznie przechodzi dialog do mainchatu (line 55 w DialogController)
                    //TODO:
                    //gracz nadal nie moze sie ruszac ani rozgladac
                    WaitForNormalChatToFinish(); //Czekaj az przejdzie przez normalChat i jak przejdzie to StartGameplay
                    break;
                case GameStates.StartGameplay:
                    //TODO:
                    UnlockPlayer();//Odblokuj ruszanie się graczowi
                    break;
                case GameStates.VictoryNPC:
                    //Jak ten state sie zaladuje, to automatycznie przechodzi dialog do exitChatu (line 59 w DialogController)
                    WaitForNpcToGoAway();
                    
                    break;
                case GameStates.LoseNPC:
                    
                    break;
                case GameStates.EndDay:
                    
                    break;
                default:
                    Debug.LogError("Critical error");
                    Debug.LogError("State not found");
                    break;
            }
        }

        private void LockPlayer()
        {
            _playerMovement.MouseRotationIsOn = false;
            Debug.LogWarning("zablokuj jeszcze movement");
        }

        private void UnlockPlayer()
        {
            _playerMovement.MouseRotationIsOn = true;
            Debug.LogWarning("odblokuj movement");
        }
        
        private void StartNPC()
        {
            
        }

        private void StartChat()
        {
            //test only
            TestFill.Instance.FillChat();
        }

        private void WaitForNormalChatToFinish()
        {
            StartCoroutine(WaitForNormalChatToFinishCor());
        }

        private IEnumerator WaitForNormalChatToFinishCor()
        {
            if (dialogController == null)
            {
                Debug.LogError("Ustaw w inspektorze DialogControler do GameManager");
                yield break;
            }
            yield return new WaitUntil(() => dialogController.NormalChatFinished);
            
            ChangeGameState(GameStates.StartGameplay);
        }
        
        private void WaitForNPCToSit()
        {
            StartCoroutine(WaitForNpcToSitCor());
        }

        private IEnumerator WaitForNpcToSitCor()
        {
            yield return new WaitUntil(() => NpcSatDown);

            NpcSatDown = false;
            
            ChangeGameState(GameStates.NPCMainDialog);
        }

        private void WaitForPlayerAndNPCToSitDownAndLockPlayer()
        {
            StartCoroutine(WaitForPlayerToSitOnAChair());
        }

        private IEnumerator WaitForPlayerToSitOnAChair()
        {
            Debug.Log("Waiting for player to sit down");
            yield return new WaitUntil(() => _playerManager.AmSitting);
            LockPlayer();
            
            Debug.Log("Waiting for NPC to sit down");
            yield return new WaitUntil(() => NpcSatDown);
            NpcSatDown = false;

            ChangeGameState(GameStates.NPCMainDialog);
        }
        
        private void WaitForPlayerToSitAndLockPlayer(GameStates toState)
        {
            StartCoroutine(WaitForPlayerToSitOnAChair(toState));
        }
        
        private IEnumerator WaitForPlayerToSitOnAChair(GameStates toState)
        {
            Debug.Log("Waiting for player to sit down");
            yield return new WaitUntil(() => _playerManager.AmSitting);
            LockPlayer();
            
            ChangeGameState(toState);
        }

        private void WaitForNpcToGoAway()
        {
            StartCoroutine(WaitForNpcToGoAwayCor());
        }

        private IEnumerator WaitForNpcToGoAwayCor()
        {
            Debug.Log("Waiting for npc to go away");
            yield return new WaitUntil(() => NpcWentAway);
            npcWentAway = false;
            
            ChangeGameState(GameStates.StartNextNPC);
        }

        #region TestArea

        [Button]
        public void GivePotion()
        {
            ChangeGameState(GameStates.VictoryNPC);
        }
        
        [Button]
        public void NpcSitDown()
        {
            NpcSatDown = true;
        }
        
        [Button]
        public void NpcGoAway()
        {
            NpcWentAway = true;
        }

        #endregion
    }
}

public enum GameStates
{
    StartGame, //oczekuj az player usiadzie
    StartNextNPC, //NPC sie pojawia
    StartChat, //start czatu
    NPCMainDialog, //normal czat
    StartGameplay, //przygotowanie mikstury i danie jej npcowi
    VictoryNPC, //endczat, wyleczylismy
    LoseNPC, //npc umiera
    EndDay,
}