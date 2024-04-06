using UnityEngine;

namespace GameManager
{
    public class ActivateMenu : MonoBehaviour
    {
        private PlayerInputs _playerInputs;

        private void Awake()
        {
            _playerInputs = new PlayerInputs();

            _playerInputs.Menu.Exit.performed += context => Menu();
        }

        #region [Initialization]
        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();
        }
#endregion

        private void Menu()
        {
            var isPaused = PauseGame.PauseGame.instance._isPaused;
            PauseGame.PauseGame.instance.SetPause(!isPaused);
        }
    }
}