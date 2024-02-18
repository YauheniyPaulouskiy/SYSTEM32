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

        public void Move()
        {
            
        }
    }
}