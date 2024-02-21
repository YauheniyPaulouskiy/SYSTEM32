using UnityEngine;

namespace Player.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttackAnimation : MonoBehaviour
    {
        private Animator _playerAnimator;

        #region [Initialization]
        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
        }
        #endregion

        public void AttackAnimation()
        {
            _playerAnimator.SetTrigger("Attack");
        }
    }
}