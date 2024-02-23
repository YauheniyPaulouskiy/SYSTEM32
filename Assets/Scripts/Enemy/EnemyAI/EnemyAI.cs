using Zenject;
using Death;
using Enemy.Animation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Player.Controller;
using System.Collections;

namespace Enemy.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float timer;

        [Header("Target Move")]
        [SerializeField] private PlayerMover _player;

        [Header("Sword Trigger")]
        [SerializeField] private BoxCollider _swordTriggerZone;

        private bool _isMove = true;
        private bool _isAttack = false;
        private bool _isDeath = false;

        private NavMeshAgent _enemyAgent;

        private EnemyAnimation _enemyAnimation;
        private EnemyRagdoll _enemyRagdoll;

        private IDeath[] _enemyDeathConditions;

        [HideInInspector]
        public UnityEvent<EnemyAI> DeathEnemy;

        #region [Initialization]
        private void Awake()
        {
            _enemyAgent = GetComponent<NavMeshAgent>();
            _enemyDeathConditions = GetComponents<IDeath>();
            _enemyRagdoll = GetComponent<EnemyRagdoll>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
        }
        #endregion

        private void Start()
        {
            EnableTriggerZone(_isAttack);
            SubscribeToIDeath();
        }

        private void Update()
        {
            Move();
            Attack();  
        }

        [Inject]
        private void Construct(PlayerMover player)
        {
            _player = player;
        }

        private void Move()
        {
            if (!_isAttack && !_isDeath)
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

        private void Attack()
        {
            var playerDistance = Vector3.Distance(_player.transform.position, transform.position);
            if (playerDistance < _enemyAgent.stoppingDistance && !_isAttack && !_isDeath)
            {
                _isAttack = true;
                EnableTriggerZone(_isAttack);
                _enemyAnimation.AttackAnimation();
                return;
            }
            else
            {
                _isAttack = false;
            }     
            
            EnableTriggerZone(_isAttack);
        }

        private void EnableTriggerZone(bool isAttack)
        {
            _swordTriggerZone.enabled = isAttack;
        }

        private void Death()
        {
            _enemyAnimation.SetAnimator().enabled = false;
            _enemyRagdoll.Enable();
            _isDeath = true;
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

        public void StandUp()
        {
            _enemyRagdoll.Disable();
            _enemyAnimation.SetAnimator().enabled = true;
        }

        private IEnumerator DeathTime()
        {
            yield return new WaitForSeconds(timer);
            DeathEnemy.Invoke(this);
        }
    }  
}