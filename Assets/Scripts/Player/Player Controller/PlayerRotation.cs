using UnityEngine;

namespace Player.Controller
{
    public class PlayerRotation : MonoBehaviour
    {
        [Header("Sensetivity Value")]
        [SerializeField] private float _mouseSensetivity;

        [Header("Min/Max Rotation Angle")]
        [SerializeField] private Vector2 _minMaxHorizontalAngle;
        [SerializeField] private Vector2 _minMaxVerticalAngle;

        [Header("Player Rotation Object")]
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private Transform _playerTransform;

        private float _xRotation;
        private float _yRotation;

        private PlayerInputs _playerInputs;

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

        private void Update()
        {
            CameraBinding();
            Rotation();
        }

        private void CameraBinding()
        {
            _playerCamera.position = transform.position;
            _playerCamera.localRotation = transform.rotation;
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

            var yRotation = transform.localEulerAngles.y;
            var repeatAngle = Mathf.Repeat(180f + yRotation, 360f) - 180f;

            if (Mathf.Abs(repeatAngle) >= _minMaxVerticalAngle.y)
            {
                _playerTransform.Rotate(Vector3.up * scaleRotation.x);
            }
        }
    }
}