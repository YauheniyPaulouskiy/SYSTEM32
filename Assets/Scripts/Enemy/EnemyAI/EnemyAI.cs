using Zenject;
using Death;
using Enemy.Animation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Player.Controller;
using System.Collections;
using GameManager.PauseGame;

namespace Enemy.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("Raycast Value")]
        [SerializeField] private float _maxDistanceFromPlayer;
        [SerializeField] private float _originOffset;

        [Header("Timer Value")]
        [SerializeField] private float _timerSeconds;

        [Header("Target Move")]
        [SerializeField] private PlayerMover _player;

        [Header("Sword Trigger")]
        [SerializeField] private BoxCollider _swordTriggerZone;

        [Header("Player Mask")]
        [SerializeField] private LayerMask _playerMask;

        private bool _isMove = true;
        private bool _isDeath = false;
        private bool _isPlayer;

        private NavMeshAgent _enemyAgent;

        private EnemyAnimation _enemyAnimation;

        private IDeath[] _enemyDeathConditions;

        [HideInInspector]
        public UnityEvent<EnemyAI> DeathEnemy;
        public UnityEvent DeathEnemyScore;

        #region [Initialization]
        private void Awake()
        {
            _enemyAgent = GetComponent<NavMeshAgent>();
            _enemyDeathConditions = GetComponents<IDeath>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
        }

        [Inject]
        private void Construct(PlayerMover player)
        {
            _player = player;
        }
        #endregion

        private void Start()
        {
            SubscribeToIDeath();
            ActivateTriggerZone(false);
        }

        private void Update()
        {
            if (PauseGame.instance._isPaused)
            {
                _enemyAgent.SetDestination(transform.position);
                return;
            }

            Move();
            StartAttack();  
        }

        private void FixedUpdate()
        {
            if (PauseGame.instance._isPaused)
            {
                return;
            }

            PlayerCheck();
        }

        private void Move()
        {
            if (!_isDeath)
            {
                _isMove = true;
                _enemyAgent.SetDestination(_player.transform.position);
            }
            else
            {
                _isMove = false;
            }

            _enemyAnimation.MoveAnimation(_isMove);
        }

        private void StartAttack()
        {          
            if (_isPlayer && !_isDeath)
            {
                ActivateTriggerZone(true);

                _enemyAnimation.AttackAnimation();
            }     
        }

        private void EndAttack()
        {
            ActivateTriggerZone(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ActivateTriggerZone(false);
            }
        }

        private void ActivateTriggerZone(bool isAttack)
        {
            _swordTriggerZone.enabled = isAttack;
        }

        public void StandUp()
        {
            _enemyAnimation.RespawnAnimation();
        }

        private void Death()
        {
            _isDeath = true;

            ActivateTriggerZone(false);

            _enemyAnimation.DeathAnimation();

            DeathEnemyScore?.Invoke();

            StartCoroutine(DeathTime());
        }

        private void SubscribeToIDeath()
        {
            foreach (var enemyDeath in _enemyDeathConditions)
            {
                enemyDeath.Death += Death;
            }
        } 
        
        public void GetIsDeath(bool isDeath)
        {
            _isDeath = isDeath;
        }

        private void PlayerCheck()
        {
            var curPosition = transform.position;
            curPosition.y += _originOffset;
            var direction = transform.forward;

            _isPlayer = Physics.Raycast(curPosition, direction, _maxDistanceFromPlayer, _playerMask);
        }

        #region [Timer]
        private IEnumerator DeathTime()
        {
            yield return new WaitForSeconds(_timerSeconds);
            DeathEnemy.Invoke(this);
        }
        #endregion
    }  
}