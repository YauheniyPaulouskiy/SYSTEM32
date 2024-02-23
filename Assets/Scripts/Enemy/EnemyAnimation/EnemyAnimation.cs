using UnityEngine;

namespace Enemy.Animation
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimation : MonoBehaviour
    {
        [Header("Animation Objects")]
        private Animator _enemyAnimator;

        #region [Initialization]
        private void Awake()
        {
            _enemyAnimator = GetComponent<Animator>();
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

        public Animator SetAnimator()
        {
            return _enemyAnimator;
        }
    }
}