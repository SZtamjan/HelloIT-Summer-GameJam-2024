using System;
using Player;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class GameTimeScaleController : MonoBehaviour
    {
        private MenuManager _menuManager;

        public UnityEvent pausedGame;
        public bool gamePaused = false;

        private void Start()
        {
            _menuManager = MenuManager.Instance;
        }

        public void SwitchPauseFromInput(InputAction.CallbackContext ctx)
        {
            if(ctx.started) SwitchPause();
        }
        
        public void SwitchPause()
        {
            pausedGame.Invoke();
            
            if (Mathf.RoundToInt(Time.timeScale) == 1)
            {
                _menuManager.SwitchMenuVis(true);
                gamePaused = true;
                Time.timeScale = 0f;
                return;
            }

            gamePaused = false;
            _menuManager.SwitchMenuVis(false);
            Time.timeScale = 1f;
        }
    }
}