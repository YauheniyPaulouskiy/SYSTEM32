using System.Collections;
using UI.Text;
using UnityEngine;
using GameManager.PauseGame;

namespace Player.UI
{
    public class PlayerDeathScreen : MonoBehaviour, IPausedFromDeathHandler
    {
        [SerializeField] private float _secondTimers;

        [SerializeField] private Transform _deathBackground;
        [SerializeField] private ScrambleText _deathText;
        [SerializeField] private Transform _exitButton;

        #region[Initialization]
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
            _deathBackground.gameObject.SetActive(false); 
            _exitButton.gameObject.SetActive(false);
        }

        private void Activate(bool isPausedFromDeath)
        {
            if (isPausedFromDeath)
            {
                _deathBackground.gameObject.SetActive(isPausedFromDeath);
                _deathText.StartAnimation();
                StartCoroutine(ActivateExitButtonRutine(isPausedFromDeath));
            }          
        }

        private IEnumerator ActivateExitButtonRutine(bool isPausedFromDeath)
        {
            var waitForSeconds = _deathText.GetTimeDuration() + _secondTimers;
            yield return new WaitForSeconds(waitForSeconds);
            _exitButton.gameObject.SetActive(isPausedFromDeath);
        }

        public void IsPausedFromDeath(bool isPausedFromDeath)
        {
            Activate(isPausedFromDeath);
        }
    }
}