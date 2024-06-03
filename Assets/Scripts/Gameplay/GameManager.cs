using System;
using System.Collections;
using NaughtyAttributes;
using NPC;
using Player;
using Player.Movement;
using UI;
using UIDialog;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private GameStates _gameStates;

        public static event Action<GameStates> OnGameStateChange;

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

        #endregion Properties

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
                    StartCoroutine(WaitForPlayerToSitOnAChair(GameStates.StartNextNPC));
                    break;

                case GameStates.StartNextNPC:
                    StartNPC(); //NPC WalksIn - EMPTY Method // zrobione
                    ChangeGameState(GameStates.StartChat); //StartChat

                    break;

                case GameStates.StartChat:
                    StartChat(); //zczytaj z npc czat z jego SO // zriobione
                    //WaitForNPCToSit(); //Czekaj aż npc usiądzie
                    StartCoroutine(WaitForPlayerToSitOnAChair());
                    break;

                case GameStates.NPCMainDialog:
                    //Jak ten state sie zaladuje, to automatycznie przechodzi dialog do mainchatu (line 55 w DialogController)
                    //TODO:
                    //gracz nadal nie moze sie ruszac ani rozgladac
                    StartCoroutine(WaitForNormalChatToFinishCor()); //Czekaj az przejdzie przez normalChat i jak przejdzie to StartGameplay
                    break;

                case GameStates.StartGameplay:
                    //TODO:
                    UnlockPlayer();//Odblokuj ruszanie się graczowi
                    break;

                case GameStates.VictoryNPC:
                    //Jak ten state sie zaladuje, to automatycznie przechodzi dialog do exitChatu (line 59 w DialogController)
                    StartCoroutine(WaitForNpcToGoAwayCor());
                    break;

                case GameStates.LoseNPC:

                    break;

                case GameStates.EndDay:
                    Debug.Log("EndDay");
                    // Włacz podsumowanie
                    UIController.Instance.WlaczDziennik(true);
                    //NPCController.Instance.NextDay();
                    //WlaczSklep();
                    //ChangeGameState(GameStates.StartGame);
                    // ↑ Przeniesione do włącz sklep ↑
                    break;

                case GameStates.KoniecGry:
                    //jakiś koniec Gry zaraz robie
                    Debug.Log("End Game");
                    UIController.Instance.WlaczDziennik(true);
                    break;

                default:
                    Debug.LogError("Critical error");
                    Debug.LogError("State not found");
                    break;
            }
            //ChangeGameState(GameStates.WaitState);
        }

        public void WlaczSklep()
        {
            NPCController.Instance.NextDay();
            ChangeGameState(GameStates.StartGame);
            UIController.Instance.ActualShop.SetActive(true);
            UIController.Instance.UpdateButtons();
        }

        private void LockPlayer()
        {
            _playerMovement.MouseRotationIsOn = false;
            _playerMovement.PlayerMovementIsOn = false;
            StartCoroutine(_playerMovement.TurnPlayerTowardsNPC());
        }

        private void UnlockPlayer()
        {
            _playerMovement.MouseRotationIsOn = true;
            _playerMovement.PlayerMovementIsOn = true;
            _playerManager.AmSitting = false;
        }

        private void StartNPC()
        {
            NPCController.Instance.NastepnyPacjent();
        }

        private void StartChat()
        {
            //test only //TestFill.Instance.FillChat();
            var currnentNPC = NPCController.Instance.GetCurrnetPacjent();
            DialogController.Instance.ProcessThroughChats(currnentNPC.EntryChatList(), currnentNPC.NormalChatList(), currnentNPC.ExitChatList());
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

        private IEnumerator WaitForPlayerToSitOnAChair(GameStates toState)
        {
            Debug.Log("Waiting for player to sit down");
            yield return new WaitUntil(() => _playerManager.AmSitting);
            LockPlayer();

            ChangeGameState(toState);
        }

        private IEnumerator WaitForNpcToGoAwayCor()
        {
            Debug.Log("Waiting for npc to go away");
            NPCController.Instance.KoniecPacjenta();
            yield return new WaitUntil(() => NpcWentAway);

            npcWentAway = false;
            if (NPCController.Instance.KoniecDni())
            {
                ChangeGameState(GameStates.KoniecGry);
            }
            else if (NPCController.Instance.KonieKolejki())
            {
                ChangeGameState(GameStates.EndDay);
            }
            else
            {
                ChangeGameState(GameStates.StartNextNPC);
            }
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

        #endregion TestArea
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
    KoniecGry
}