using System;
using System.Collections;
using Player;
using Player.Movement;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private GameStates _gameStates;

        public GameStates GameStates
        {
            get => _gameStates;
            private set => _gameStates = value;
        }

        //Components
        private PlayerManager _playerManager;
        private PlayerMovement _playerMovement;

        //Vars

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
        }

        public void ChangeGameState(GameStates newState)
        {
            Debug.LogWarning(newState);
            switch (newState)
            {
                case GameStates.StartGame:
                    WaitForPlayerToSit();
                    break;
                case GameStates.StartNPC:
                    //movement jest zlockowany ze do StartGameplay nie bedzie gracz mogl wstac
                    //tak samo dlugo ma kamera sie nie ruszac
                    _playerMovement.MouseRotationIsOn = false;
                    //Ustaw gracza, wylacz kamere itp
                    StartChatWithNPC();
                    break;
                case GameStates.StartChat:
                    //zczytaj z npc czat z jego SO
                    break;
                case GameStates.StartGameplay:
                    
                    break;
                default:
                    Debug.LogError("Critical error");
                    Debug.LogError("State not found");
                    break;
            }
        }

        private void StartChatWithNPC()
        {
            ChangeGameState(GameStates.StartChat);
        }

        private void WaitForPlayerToSit()
        {
            StartCoroutine(WaitForPlayerToSitOnAChair());
        }

        private IEnumerator WaitForPlayerToSitOnAChair()
        {
            yield return new WaitUntil(() => _playerManager.AmSitting);
            
            ChangeGameState(GameStates.StartNPC);
        }
        
    }
}

public enum GameStates
{
    StartGame,
    StartNPC,
    StartChat,
    StartGameplay,
}