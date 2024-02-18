using Player.Animation;
using UnityEngine;

namespace Player.Controller
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent (typeof(PlayerRotationAnimation))]
    public class PlayerRotation : MonoBehaviour
    {
        [Header("Animation Scripts")]
        [SerializeField] private PlayerRotationAnimation _playerRotationAnimation;

        #region [Initialization]
        private void Awake()
        {
            _playerRotationAnimation = GetComponent<PlayerRotationAnimation>();
        }
        #endregion

        public void Rotation(float CameraRotationY, float maxVerticalAngle, float scaleRotationX, float InputsRotationX)
        {
            var yRotation = CameraRotationY;
            var repeatAngle = Mathf.Repeat(180f + yRotation, 360f) - 180f;

            if (Mathf.Abs(repeatAngle) >= maxVerticalAngle)
            {
                transform.Rotate(Vector3.up * scaleRotationX);
                _playerRotationAnimation.Rotation(InputsRotationX);
            }
        }
    }
}