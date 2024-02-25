using GameManager.PauseGame;
using UnityEngine;

namespace GameManager.Mouse
{
    public class MouseActive : MonoBehaviour, IPausedHandler
    {
        #region [Initialization]
        private void OnEnable()
        {
            PauseGame.PauseGame.instance.AddList(this);
        }

        private void OnDisable()
        {
            PauseGame.PauseGame.instance.RemoveList(this);
        }
        #endregion

        private void Start()
        {
            IsActivate(false);
        }

        private void IsActivate(bool isActivate)
        {
            if (isActivate)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        private void Enable()
        {
            Cursor.lockState = CursorLockMode.None ;
            Cursor.visible = true;
        }

        private void Disable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void IsPaused(bool isPaused)
        {
            IsActivate(isPaused);
        }
    }
}