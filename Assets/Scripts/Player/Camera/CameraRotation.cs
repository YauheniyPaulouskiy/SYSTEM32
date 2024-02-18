using Player.Controller;
using UnityEngine;

namespace Player.Camera
{
    public class CameraRotation : MonoBehaviour
    {
        [Header("Sensetivity Value")]
        [SerializeField] private float _mouseSensetivity;

        [Header("Min/Max Rotation Angle")]
        [SerializeField] private Vector2 _minMaxHorizontalAngle;
        [SerializeField] private Vector2 _minMaxVerticalAngle;

        [Header("Player Rotation Object")]
        [SerializeField] private PlayerRotation _playerRotation;

        private float _xRotation;
        private float _yRotation;

        private PlayerInputs _playerInputs;

        #region [Initialization]
        private void Awake()
        {
            _playerInputs = new PlayerInputs();
        }

        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();
        }
        #endregion

        private void Update()
        {
            Rotation();
        }

        private void Rotation()
        {
            var inputsRotation = _playerInputs.Player.Rotation.ReadValue<Vector2>();
            var scaleRotation = inputsRotation * _mouseSensetivity * Time.deltaTime;

            _xRotation -= scaleRotation.y;
            _xRotation = Mathf.Clamp(_xRotation, _minMaxHorizontalAngle.x, _minMaxHorizontalAngle.y);

            _yRotation += scaleRotation.x;
            _yRotation = Mathf.Clamp(_yRotation, _minMaxVerticalAngle.x, _minMaxVerticalAngle.y);

            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);

            _playerRotation.Rotation(transform.localEulerAngles.y, _minMaxVerticalAngle.y, scaleRotation.x, inputsRotation.x);
        }
    }
}