using UnityEngine;

namespace Player.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerRotationAnimation : MonoBehaviour
    {
        [Header("Animation Objects")]
        [SerializeField] private Animator _playerAnimator;

        #region [Initialization]
        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
        }
        #endregion

        public void Rotation(float directionRotation)
        {
            _playerAnimator.SetFloat("directionRotation", directionRotation);
        }
    }
}