using GameManager.PauseGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.UI
{
    public class MenuScreen : MonoBehaviour, IPausedHandler
    {
        [SerializeField] private Transform _menuPanel;

        #region [Initialization]
        private void OnEnable()
        {
            PauseGame.instance.AddList(this);
        }

        private void OnDisable()
        {
            PauseGame.instance.RemoveList(this);
        }
        #endregion

        private void Start()
        {
            _menuPanel.gameObject.SetActive(false);
        }

        private void Activate(bool isPaused)
        {
            _menuPanel.gameObject.SetActive(isPaused);
        }

        public void ResumeGame()
        {
            PauseGame.instance.SetPause(false);
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(0);
        }

        public void IsPaused(bool isPaused)
        {
            Activate(isPaused);
        }
    }
}