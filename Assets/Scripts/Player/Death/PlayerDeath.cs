using Death;
using GameManager.PauseGame;
using Player.Animation;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Death
{
    public class PlayerDeath : MonoBehaviour
    {
        [Header("HealthPoint Value")]
        [SerializeField] private float _hp;
        [SerializeField] private float _maxHp;
        [SerializeField] private float _changeHp;
        [SerializeField] private float _reloadHealthTimer;

        private bool _isDeath;

        private Coroutine _coroutine;

        private PlayerAnimation _playerAnimation;

        private IDeath[] _playerDeathConditions;

        [HideInInspector]
        public UnityEvent DeathPlayer = new UnityEvent();

        #region [Initialization]
        private void Awake()
        {
            _playerDeathConditions = GetComponents<IDeath>();
            _playerAnimation = GetComponent<PlayerAnimation>();
        }
        #endregion

        private void Start()
        {
            SubscribeToDeath();
        }

        private void SubscribeToDeath()
        {
            foreach (var playerDeath in _playerDeathConditions)
            {
                playerDeath.Death += DecreaseHealth;
            }
        }

        private void RestoringHealth()
        {
            _hp += 1;

            isDeath();
        }

        private void DecreaseHealth()
        {
            _hp -= _changeHp;

            StartAndStopRutine();
            isDeath();
        }

        private void isDeath()
        {
            if (_hp <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            if (!_isDeath) 
            {
                _isDeath = true;
                PauseGame.instance.SetPause(true, true);
            }           
        }

        #region [Timer]
        private void StartAndStopRutine()
        {
            if (_coroutine == null)
            {
                AddingRutine();
            }
            else
            {
                StopCoroutine(_coroutine);
                AddingRutine();
            }
        }

        private void AddingRutine()
        {
            _coroutine = StartCoroutine(IReloadHealth());
        }

        private IEnumerator IReloadHealth()
        {
            while (_hp < _maxHp && !PauseGame.instance._isPaused)
            {
                yield return new WaitForSeconds(_reloadHealthTimer);

                RestoringHealth();
            }
        }
        #endregion
    }
}