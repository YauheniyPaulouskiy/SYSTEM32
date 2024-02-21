using UnityEngine;

namespace Player.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMoveAnimation : MonoBehaviour
    {
        [Header("Animation Objects")]
        [SerializeField] private Animator _playerAnimator;

        #region [Initialization]
        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
        }
        #endregion

        public void MoveAnimation(Vector2 moveDirection)
        {
            _playerAnimator.SetFloat("Vertical", moveDirection.x);
            _playerAnimator.SetFloat("Horizontal", moveDirection.y);
        }
    }
}