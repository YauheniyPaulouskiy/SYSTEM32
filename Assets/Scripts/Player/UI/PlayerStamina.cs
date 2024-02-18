using System.Collections;
using UnityEngine;

namespace Player.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PlayerStamina : MonoBehaviour
    {
        [Header("Timer Value")]
        [SerializeField] private float _returnSeconds = 3f;

        [Header("Fill Area")]
        [SerializeField] private RectTransform _fillRect;

        [Header("Canvas Group")]
        [SerializeField] private CanvasGroup _canvasGroup;

        private float _staminaValue;
        private float _maxStaminaValue;

        private Coroutine staminaRestartTime;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {

        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;

            _staminaValue = _maxStaminaValue;
        }

        private void ChangeStamina(float stamina)
        {
            _staminaValue -= stamina;
            Stamina();
        }

        private void Stamina()
        {
            var staminaSize = _staminaValue / _maxStaminaValue;
            _fillRect.localScale = new Vector3(staminaSize, 1, 1);
        }

        #region [TimeRutine]
        private void StartAndStopRutine()
        {
            if (staminaRestartTime == null)
            {
                StartRutine();
            }
            else
            {
                StopCoroutine(staminaRestartTime);
                StartRutine();
            }
        }

        private void StartRutine()
        {
            staminaRestartTime = StartCoroutine(IRecoveryTime());
        }

        private IEnumerator IRecoveryTime()
        {
            yield return new WaitForSeconds(_returnSeconds);
        }
        #endregion
    }
}
