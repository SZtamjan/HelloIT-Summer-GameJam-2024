using System;
using Player;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class GameTimeScaleController : MonoBehaviour
    {
        private MenuManager _menuManager;

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
            if (Mathf.RoundToInt(Time.timeScale) == 1)
            {
                _menuManager.SwitchMenuVis(true);
                Time.timeScale = 0f;
                return;
            }

            _menuManager.SwitchMenuVis(false);
            Time.timeScale = 1f;
        }
    }
}