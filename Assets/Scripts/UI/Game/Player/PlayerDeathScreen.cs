using System.Collections;
using UI.Text;
using UnityEngine;
using GameManager.PauseGame;
using Zenject;
using Player.Controller;
using Player.Death;

namespace Player.UI
{
    public class PlayerDeathScreen : MonoBehaviour
    {
        [SerializeField] private float _secondTimers;

        [SerializeField] private Transform _deathBackground;
        [SerializeField] private ScrambleText _deathText;
        [SerializeField] private Transform _exitButton;

        private PlayerDeath _player;

        #region[Initialization]
        private void OnEnable()
        {
            _player.DeathPlayer.AddListener(Activate);
        }

        private void OnDisable()
        {
            _player.DeathPlayer.RemoveListener(Activate);
        }

        [Inject]
        private void Container(PlayerMover player)
        {
            _player = player.GetComponent<PlayerDeath>();
        }
        #endregion

        private void Start()
        {
            _deathBackground.gameObject.SetActive(false); 
            _exitButton.gameObject.SetActive(false);
        }

        private void Activate()
        {
            var isPaused = PauseGame.instance._isPaused;

            _deathBackground.gameObject.SetActive(isPaused);
            _deathText.StartAnimation();
            StartCoroutine(ActivateExitButtonRutine(isPaused));      
        }

        private IEnumerator ActivateExitButtonRutine(bool isPaused)
        {
            var waitForSeconds = _deathText.GetTimeDuration() + _secondTimers;
            yield return new WaitForSeconds(waitForSeconds);
            _exitButton.gameObject.SetActive(isPaused);
        }
    }
}