using GameManager.PauseGame;
using UnityEngine;

namespace Enemy.Animation
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimation : MonoBehaviour, IPausedHandler
    {
        private Animator _enemyAnimator;
        private EnemyRagdoll _enemyRagdoll;

        #region [Initialization]
        private void Awake()
        {
            _enemyAnimator = GetComponent<Animator>();
            _enemyRagdoll = GetComponent<EnemyRagdoll>();
        }

        private void OnEnable()
        {
            PauseGame.instance.AddList(this);
        }

        private void OnDisable()
        {
            PauseGame.instance.RemoveList(this);
        }
        #endregion

        public void MoveAnimation(bool isMove)
        {
            _enemyAnimator.SetBool("isMove", isMove);
        }

        public void AttackAnimation()
        {
            _enemyAnimator.SetTrigger("Attack");
        }

        public void RespawnAnimation()
        {
            _enemyRagdoll.IsActivate(false);
            _enemyAnimator.enabled = true;
        }

        public void DeathAnimation()
        {
            _enemyAnimator.enabled = false;
            _enemyRagdoll.IsActivate(true);
        }

        public void IsPaused(bool isPaused)
        {
            _enemyAnimator.enabled = !isPaused;
            _enemyRagdoll.IsActivate(!isPaused);
        }
    }
}