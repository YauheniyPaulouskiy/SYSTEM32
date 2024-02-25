using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace UI.Text
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScrambleText : MonoBehaviour
    {
        [Header("Timer Value")]
        [SerializeField] private float _timeDuration;

        [Header("Text")]
        [SerializeField] private string _deathText;

        private StringBuilder _deathTextStringBuilder;

        private TMP_Text _text;

        private Coroutine _coroutine;

        #region [Initialization]
        private void Awake()
        {
            _deathTextStringBuilder = new StringBuilder();
            _text = GetComponent<TMP_Text>();
        }
        #endregion

        public float GetTimeDuration()
        {
            return _timeDuration;
        }

        public void StartAnimation()
        {
            ReloadText();
            StartAndStopRutine();
        }

        private void ReloadText()
        {
            _text.text = "";
            _deathTextStringBuilder.Clear();

            var alphabet = new StringBuilder();
            for (int i = 65; i <= 122; i++)
            {
                if ((i > 64 && i < 91) || (i > 96 && i < 123))
                {
                    alphabet.Append(Convert.ToChar(i));
                }
            }

            for (int i = 0; i < _deathText.Length; i++)
            {
                var randomCharValue = UnityEngine.Random.Range(0, alphabet.Length);
                var randomChar = alphabet[randomCharValue];

                _deathTextStringBuilder.Append(randomChar);
            }
        }

        #region [Timer]
        private void StartAndStopRutine()
        {
            if (_coroutine == null)
            {
                ApplyRutine();
            }
            else
            {
                StopCoroutine(_coroutine);
                ApplyRutine();
            }
        }
        private void ApplyRutine()
        {
            _coroutine = StartCoroutine(TextAnimationRutine());
        }

        private IEnumerator TextAnimationRutine()
        {
            var timeDurationForChar = _timeDuration / _deathText.Length;
            for (int i = 0; i < _deathText.Length; i++)
            {
                yield return new WaitForSeconds(timeDurationForChar);

                _deathTextStringBuilder[i] = _deathText[i];
                _text.text = _deathTextStringBuilder.ToString();
            }
        }
        #endregion
    }
}