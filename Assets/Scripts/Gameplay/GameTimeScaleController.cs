using UI;
using UIScripts;
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
                ReactionToUI.Instance.UnlockAndShowCursor();
                ReactionToUI.Instance.LockMouseAndMovement();
                
                ReactionToUI.Instance.blockers.Add(this.SwitchPause);
                //Debug.Log("Dlugosc blockers listy " + ReactionToUI.Instance.blockers.Count);
                
                _menuManager.SwitchMenuVis(true);
                gamePaused = true;
                Time.timeScale = 0f;
                return;
            }

            ReactionToUI.Instance.blockers.Remove(this.SwitchPause);
            //Debug.Log("Dlugosc blockers listy " + ReactionToUI.Instance.blockers.Count);
            
            ReactionToUI.Instance.LockAndHideCursor();

            if (ReactionToUI.Instance.blockers.Count == 0)
            {
                ReactionToUI.Instance.UnlockMouseAndMovement();
            }

            gamePaused = false;
            _menuManager.SwitchMenuVis(false);
            Time.timeScale = 1f;
        }
    }
}